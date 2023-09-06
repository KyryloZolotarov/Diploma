namespace Order.Hosts.Services.Interfaces
{
    public interface IOrderOrderService
    {
        Task<int?> Add(int userId);
        Task<int?> Update(int userId);
        Task<int?> Delete(int id);
    }
}
