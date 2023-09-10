using Order.Host.Data;
using Order.Hosts.Repositories.Interfaces;
using Order.Hosts.Services.Interfaces;

namespace Order.Hosts.Services;

public class OrderItemService : BaseDataService<ApplicationDbContext>, IOrderItemService
{
    private readonly IOrderItemRepository _orderItemRepository;

    public OrderItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IOrderItemRepository orderItemRepository)
        : base(dbContextWrapper, logger)
    {
        _orderItemRepository = orderItemRepository;
    }

    public async Task<int?> Add(int id, string name, decimal price, int catalogSubTypeId, int catalogModelId, int count, int orderId)
    {
        return await ExecuteSafeAsync(() =>
            _orderItemRepository.Add(id, name, price, catalogSubTypeId, catalogModelId, count, orderId));
    }

    public async Task<int?> Update(int id, int itemId, string name, decimal price, int catalogSubTypeId, int catalogModelId, int count, int orderId)
    {
        return await ExecuteSafeAsync(() =>
            _orderItemRepository.Update(id, itemId, name, price, catalogSubTypeId, catalogModelId, count, orderId));
    }

    public async Task<int?> Delete(int id)
    {
        return await ExecuteSafeAsync(() => _orderItemRepository.Delete(id));
    }
}