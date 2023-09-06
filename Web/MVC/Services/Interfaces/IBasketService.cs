using MVC.ViewModels;
using MVC.ViewModels.BasketViewModels;

namespace MVC.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketIndexViewModel> GetBasket();
        Task AddItemToBasket(int itemId);
        Task<BasketIndexViewModel> ChangeItemsCountInBasket(int itemId, int itemsCount);
        Task AddItemsInBasket(int itemId, int itemsCount);
        Task<BasketIndexViewModel> DeleteItemFromBasket(int id);
        Task ClearBasket();
    }
}
