using MVC.ViewModels.OrderViewModels;

namespace MVC.Services.Interfaces;

public interface IOrderService
{
    Task<bool> AddOrder(ListOrderItemsFordDisplay order);
    Task<ListOrderItemsFordDisplay> GetOrder(int id);
    Task<ListOrdersForDisplay> GetOrderList();
}