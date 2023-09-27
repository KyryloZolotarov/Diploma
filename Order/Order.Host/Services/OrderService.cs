using Order.Host.Data;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;
using Order.Hosts.Models.ToFrontResponses;
using Order.Hosts.Repositories.Interfaces;
using Order.Hosts.Services.Interfaces;

namespace Order.Hosts.Services;

public class OrderService : BaseDataService<ApplicationDbContext>, IOrderService
{
    private readonly IMapper _mapper;
    private readonly IOrderOrderRepository _orderOrderRepository;

    public OrderService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IOrderOrderRepository orderOrderRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _orderOrderRepository = orderOrderRepository;
        _mapper = mapper;
    }

    public async Task<bool> AddOrder(CurrentUser user, ListItemsForFrontRequest order)
    {
        return await ExecuteSafeAsync(() => _orderOrderRepository.AddOrder(user, order));
    }

    public async Task<OrderOrderForFrontResponse> GetOrder(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _orderOrderRepository.GetOrder(id);
            var resultMapped = new OrderOrderForFrontResponse();
            resultMapped.Order = _mapper.Map<OrderOrderDto>(result.Order);
            resultMapped.Items = result.Items.Select(s => _mapper.Map<OrderItemDto>(s)).ToList();
            return resultMapped;
        });
    }

    public async Task<ListOrderForFrontResponse> GetOrderList(string userId)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _orderOrderRepository.GetOrderList(userId);
            var resultMapped = new ListOrderForFrontResponse();
            resultMapped.Orders = result.Orders.Select(s => _mapper.Map<OrderOrderDto>(s)).ToList();
            return resultMapped;
        });
    }
}