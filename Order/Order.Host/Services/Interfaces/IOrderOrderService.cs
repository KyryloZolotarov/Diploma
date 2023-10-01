namespace Order.Hosts.Services.Interfaces;

public interface IOrderOrderService
{
    Task<int?> Add(string userId, DateTime dateTime);
    Task<int?> Update(int id, string userId, DateTime dateTime);
    Task<int?> Delete(int id);
}