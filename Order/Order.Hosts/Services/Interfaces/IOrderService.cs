using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Responses;

namespace Order.Hosts.Services.Interfaces
{
    public interface IOrderService
    {
        Task<bool> AddOrder(OrderUserDto user, OrderItemDto orderAdding);
        Task<OrderItemDto> GetOrder(int id);
        Task<ListOrdersResponse> GetOrderList(string userId);
    }
}
