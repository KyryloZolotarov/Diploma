using Order.Hosts.Models.Requests;

namespace Order.Hosts.Repositories.Interfaces
{
    public interface IOrderOrderRepository
    {
        Task<int?> Add(string userId, DateTime dateTime);
        Task<int?> Update(string userId, DateTime dateTime);
        Task<int?> Delete(int id);
        Task<IActionResult> AddOrder(OrderOrderUserFromMVC order);
        Task<IActionResult> GetOrder(int id);
        Task<IActionResult> GetOrderList();
    }
}
