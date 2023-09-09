using Order.Hosts.Data.Entities;

namespace Order.Hosts.Services.Interfaces
{
    public interface IOrderItemService
    {
        Task<int?> Add(int id, string name, decimal price, int catalogSubTypeId, int catalogModelId, int count, int orderId);
        Task<int?> Update(int id, int itemId, string name, decimal price, int catalogSubTypeId, int catalogModelId, int count, int orderId);
        Task<int?> Delete(int id);
    }
}
