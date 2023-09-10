using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;
using Order.Hosts.Models.ToFrontResponses;

namespace Order.Hosts.Services.Interfaces;

public interface IOrderService
{
    Task<bool> AddOrder(OrderUserDto user, ListItemsForFrontRequest order);
    Task<OrderOrderForFrontResponse> GetOrder(int id);
    Task<ListOrderForFrontResponse> GetOrderList(string userId);
}