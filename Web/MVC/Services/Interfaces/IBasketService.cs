using MVC.ViewModels;

namespace MVC.Services.Interfaces
{
    public interface IBasketService
    {
        Task<Basket> GetBasket();
        Task<Basket> AddItemToBasket(int? itemId);
        Task<Basket> DeleteItemFromBasket(int? itemId);
        Task<Basket> ClearBasket();
    }
}
