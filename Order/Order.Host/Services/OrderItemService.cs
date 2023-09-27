using Infrastructure.Exceptions;
using Order.Host.Data;
using Order.Hosts.Data.Entities;
using Order.Hosts.Repositories.Interfaces;
using Order.Hosts.Services.Interfaces;

namespace Order.Hosts.Services;

public class OrderItemService : BaseDataService<ApplicationDbContext>, IOrderItemService
{
    private readonly IOrderOrderRepository _orderOrderRepository;
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderItemService(
        IOrderOrderRepository orderOrderRepository,
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IOrderItemRepository orderItemRepository)
        : base(dbContextWrapper, logger)
    {
        _orderOrderRepository = orderOrderRepository;
        _orderItemRepository = orderItemRepository;
    }

    public async Task<int?> Add(int id, string name, decimal price, int catalogSubTypeId, int catalogModelId, int count, int orderId)
    {
        var order = await ExecuteSafeAsync(() => _orderOrderRepository.CheckOrderExist(orderId));
        if (order == null)
        {
            throw new BusinessException($"Order with id: {orderId} not found");
        }

        var item = new OrderItemEntity()
        {
            Id = id,
            Name = name,
            Price = price,
            CatalogModelId = catalogModelId,
            CatalogSubTypeId = catalogSubTypeId,
            Count = count,
            OrderId = orderId
        };

        return await ExecuteSafeAsync(() => _orderItemRepository.Add(item));
    }

    public async Task<int?> Update(int id, int itemId, string name, decimal price, int catalogSubTypeId, int catalogModelId, int count, int orderId)
    {
        var itemExists = await ExecuteSafeAsync(() => _orderItemRepository.CheckItemExist(id));
        if (itemExists == null)
        {
            throw new BusinessException($"Item with id: {id} not found");
        }

        itemExists.Id = id;
        itemExists.ItemId = itemId;
        itemExists.Name = name;
        itemExists.Price = price;
        itemExists.Count = count;
        itemExists.OrderId = orderId;
        itemExists.CatalogModelId = catalogModelId;
        itemExists.CatalogSubTypeId = catalogSubTypeId;

        return await ExecuteSafeAsync(() => _orderItemRepository.Update(itemExists));
    }

    public async Task<int?> Delete(int id)
    {
        var item = await ExecuteSafeAsync(() => _orderItemRepository.CheckItemExist(id));
        if (item == null)
        {
            throw new BusinessException($"Item with id: {id} not found");
        }

        return await ExecuteSafeAsync(() => _orderItemRepository.Delete(item));
    }
}