using MVC.ViewModels;
using MVC.ViewModels.BasketViewModels;

namespace MVC.Services.Interfaces
{
    public interface IBasketService
    {
        Task<IEnumerable<BasketItemFromCatalog>> GetBasket();
        Task AddItemToBasket(int? itemId);
        Task<IEnumerable<BasketItemFromCatalog>> DeleteItemFromBasket(int? itemId);
        Task ClearBasket();
    }
}
