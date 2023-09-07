using Order.Hosts.Models.Requests;
using Order.Hosts.Models.Responses;

namespace Order.Hosts.Services.Interfaces
{
    public interface IOrderService
    {
        Task<bool> AddOrder(OrderOrderUserFromMVC order);
        Task<OrderOrderResponse> GetOrder(int id);
        Task<ListOrdersResponse> GetOrderList(string userId);
    }
}
