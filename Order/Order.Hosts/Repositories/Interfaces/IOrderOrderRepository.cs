namespace Order.Hosts.Repositories.Interfaces
{
    public interface IOrderOrderRepository
    {
        Task<int?> Add(int userId);
        Task<int?> Update(int userId);
        Task<int?> Delete(int id);
    }
}
