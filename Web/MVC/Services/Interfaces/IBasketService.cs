using MVC.ViewModels;
using MVC.ViewModels.BasketViewModels;

namespace MVC.Services.Interfaces
{
    public interface IBasketService
    {
        Task<BasketIndexViewModel> GetBasket();
        Task AddItemToBasket(int itemId);
        Task<IEnumerable<BasketItemForDisplay>> ChangeItemsCountInBasket(int itemId, int itemsCount);
        Task AddItemsInBasket(int itemId, int itemsCount);
        Task<IEnumerable<BasketItemForDisplay>> DeleteItemFromBasket(int? itemId);
        Task ClearBasket();
    }
}
