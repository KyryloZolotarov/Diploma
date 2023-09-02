using MVC.ViewModels;

namespace MVC.Services.Interfaces
{
    public interface IBasketService
    {
        Task<IEnumerable<Basket>> GetBasket();
        Task<Basket> AddItemToBasket(int? itemId);
        Task<Basket> DeleteItemFromBasket(int? itemId);
        Task<Basket> ClearBasket();
    }
}
