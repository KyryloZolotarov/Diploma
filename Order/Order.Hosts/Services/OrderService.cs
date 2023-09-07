using Order.Hosts.Models.Requests;
using Order.Hosts.Models.Responses;
using Order.Hosts.Services.Interfaces;

namespace Order.Hosts.Services
{
    public class OrderService : IOrderService
    {
        public Task<bool> AddOrder(OrderOrderUserFromMVC order)
        {
            throw new NotImplementedException();
        }

        public Task<OrderOrderResponse> GetOrder(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ListOrdersResponse> GetOrderList(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
