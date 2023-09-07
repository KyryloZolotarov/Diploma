using Order.Hosts.Data.Entities;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Responses;

namespace Order.Hosts.Repositories.Interfaces
{
    public interface IOrderOrderRepository
    {
        Task<int?> Add(string userId, DateTime dateTime);
        Task<int?> Update(string userId, DateTime dateTime);
        Task<int?> Delete(int id);
        Task<bool> AddOrder(OrderUserDto user, OrderItemDto orderAdding);
        Task<OrderItemEntity> GetOrder(int id);
        Task<OrderOrderResponse> GetOrderList(string userId);
    }
}
