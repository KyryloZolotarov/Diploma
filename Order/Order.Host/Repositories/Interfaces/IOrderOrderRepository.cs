using Order.Hosts.Models.BaseResponses;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;

namespace Order.Hosts.Repositories.Interfaces;

public interface IOrderOrderRepository
{
    Task<int?> Add(string userId, DateTime dateTime);
    Task<int?> Update(string userId, DateTime dateTime);
    Task<int?> Delete(int id);
    Task<bool> AddOrder(OrderUserDto user, ListItemsForFrontRequest order);
    Task<OrderOrderResponse> GetOrder(int id);
    Task<ListOrdersResponse> GetOrderList(string userId);
}