using Order.Hosts.Data.Entities;
using Order.Hosts.Models.BaseResponses;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;

namespace Order.Hosts.Repositories.Interfaces;

public interface IOrderOrderRepository
{
    Task<int?> Add(OrderOrderEntity order);
    Task<int?> Update(OrderOrderEntity order);
    Task<int?> Delete(OrderOrderEntity order);
    Task<bool> AddOrder(CurrentUser user, ListItemsForFrontRequest order);
    Task<OrderOrderResponse> GetOrder(int id);
    Task<ListOrdersResponse> GetOrderList(string userId);
    Task<OrderOrderEntity> CheckOrderExist(int id);
}