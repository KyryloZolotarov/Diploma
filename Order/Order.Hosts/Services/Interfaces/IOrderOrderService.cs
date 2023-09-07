namespace Order.Hosts.Services.Interfaces
{
    public interface IOrderOrderService
    {
        Task<int?> Add(string userId, DateTime dateTime);
        Task<int?> Update(string userId, DateTime dateTime);
        Task<int?> Delete(int id);
    }
}
