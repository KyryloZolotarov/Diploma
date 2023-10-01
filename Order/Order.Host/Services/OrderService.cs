using System.Net;
using System.Xml.Linq;
using Infrastructure.Exceptions;
using Order.Host.Data;
using Order.Hosts.Data.Entities;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;
using Order.Hosts.Models.ToFrontResponses;
using Order.Hosts.Repositories.Interfaces;
using Order.Hosts.Services.Interfaces;

namespace Order.Hosts.Services;

public class OrderService : BaseDataService<ApplicationDbContext>, IOrderService
{
    private readonly IMapper _mapper;
    private readonly IOrderUserRepository _orderUserRepository;
    private readonly IOrderItemRepository _orderItemRepository;
    private readonly IOrderOrderRepository _orderOrderRepository;

    public OrderService(
        IOrderUserRepository orderUserRepository,
        IOrderItemRepository orderItemRepository,
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IOrderOrderRepository orderOrderRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _orderUserRepository = orderUserRepository;
        _orderItemRepository = orderItemRepository;
        _orderOrderRepository = orderOrderRepository;
        _mapper = mapper;
    }

    public async Task<bool> AddOrder(CurrentUser user, ListItemsForFrontRequest order)
    {
        var userExists = await ExecuteSafeAsync(() => _orderUserRepository.CheckUserExist(user.Id));
        if (userExists == null)
        {
            var userCreating = new OrderUserEntity()
            {
                Id = user.Id,
                Name = user.Name,
                GivenName = user.GivenName,
                FamilyName = user.FamilyName,
                Email = user.Email,
                Address = user.Address
            };
            await ExecuteSafeAsync(() => _orderUserRepository.Add(userCreating));
        }

        var orderAdding = new OrderOrderEntity
        {
            UserId = user.Id,
            DateTime = order.DateTime.ToUniversalTime()
        };

        await ExecuteSafeAsync(() => _orderOrderRepository.Add(orderAdding));

        var items = new List<OrderItemEntity>();

        foreach (var item in order.Items)
        {
            items.Add(new OrderItemEntity
            {
                ItemId = item.Id,
                Name = item.Name,
                Price = item.Price,
                CatalogModelId = item.CatalogModelId,
                CatalogSubTypeId = item.CatalogSubTypeId,
                OrderId = orderAdding.Id
            });
        }

        return await ExecuteSafeAsync(() => _orderItemRepository.AddItemsForOrder(items));
    }

    public async Task<OrderOrderForFrontResponse> GetOrder(string userId, int id)
    {
        var userExists = await ExecuteSafeAsync(() => _orderUserRepository.CheckUserExist(userId));

        var orderExists = await ExecuteSafeAsync(() => _orderOrderRepository.CheckOrderExist(id));
        if (orderExists.UserId != userExists.Id)
        {
            throw new AccessViolationException($"Access forbidden");
        }

        var result = await ExecuteSafeAsync(() => _orderItemRepository.GetItemsForOrder(orderExists.Id));
        var resultMapped = new OrderOrderForFrontResponse();
        resultMapped.Order = _mapper.Map<OrderOrderDto>(orderExists);
        resultMapped.Items = result.Select(s => _mapper.Map<OrderItemDto>(s)).ToList();
        return resultMapped;
    }

    public async Task<ListOrderForFrontResponse> GetOrderList(string userId)
    {
        var userExists = await ExecuteSafeAsync(() => _orderUserRepository.CheckUserExist(userId));
        if (userExists == null)
        {
            throw new BusinessException($"User with id: {userId} not found");
        }

        return await ExecuteSafeAsync(async () =>
        {
            var result = await _orderOrderRepository.GetOrderList(userId);
            var resultMapped = new ListOrderForFrontResponse();
            resultMapped.Orders = result.Orders.Select(s => _mapper.Map<OrderOrderDto>(s)).ToList();
            return resultMapped;
        });
    }
}