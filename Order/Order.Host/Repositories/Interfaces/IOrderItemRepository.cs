using Order.Hosts.Data.Entities;

namespace Order.Hosts.Repositories.Interfaces;

public interface IOrderItemRepository
{
    Task<int?> Add(OrderItemEntity item);

    Task<int?> Update(OrderItemEntity item);

    Task<int?> Delete(OrderItemEntity item);
    Task<OrderItemEntity> CheckItemExist(int id);
    Task<bool> AddItemsForOrder(List<OrderItemEntity> items);
    Task<List<OrderItemEntity>> GetItemsForOrder(int orderId);
}