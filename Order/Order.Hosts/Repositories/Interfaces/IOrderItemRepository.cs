namespace Order.Hosts.Repositories.Interfaces
{
    public interface IOrderItemRepository
    {
        Task<int?> Add(int id, string name, decimal price, int catalogSubTypeId, int catalogModelId, int count, int orderId);
        Task<int?> Update(int id, string name, decimal price, int catalogSubTypeId, int catalogModelId, int count, int orderId);
        Task<int?> Delete(int id);
    }
}
