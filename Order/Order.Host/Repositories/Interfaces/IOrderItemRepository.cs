using Order.Hosts.Data.Entities;

namespace Order.Hosts.Repositories.Interfaces;

public interface IOrderItemRepository
{
    Task<int?> Add(OrderItemEntity item);

    Task<int?> Update(OrderItemEntity item);

    Task<int?> Delete(OrderItemEntity item);
    Task<OrderItemEntity> CheckItemExist(int id);
}