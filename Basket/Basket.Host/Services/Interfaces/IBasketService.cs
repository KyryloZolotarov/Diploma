using Basket.Host.Models;

namespace Basket.Host.Services.Interfaces
{
    public interface IBasketService
    {
        Task Add(string basketId, string itemId);
        Task<BasketItemsDb> Get(string basketId);
        Task Delete(string basketId);
        Task<BasketItemsDb> DeleteItem(string basketId, string itemId);
    }
}
