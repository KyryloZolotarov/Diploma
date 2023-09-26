using MVC.ViewModels.BasketViewModels;

namespace MVC.Repositories.Interfaces
{
    public interface IBasketRepository
    {
        Task<BasketItemsFromBasket> GetBasket();
        Task AddItemToBasket(int itemId);
        Task<BasketItemsFromBasket> ChangeItemsCountInBasket(int itemId, int itemsCount);
        Task AddItemsInBasket(int itemId, int itemsCount);
        Task<BasketItemsFromBasket> DeleteItemFromBasket(int id);
        Task ClearBasket();
    }
}
