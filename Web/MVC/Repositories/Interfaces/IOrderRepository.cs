using MVC.Models.Requests;
using MVC.Models.Responses;

namespace MVC.Repositories.Interfaces
{
    public interface IOrderRepository
    {
        Task<bool> AddOrder(ListOrderItemsRequest order);
        Task<ListOrderItemsResponse> GetOrder(int id);
        Task<ListOrderResponse> GetOrderList();
    }
}
