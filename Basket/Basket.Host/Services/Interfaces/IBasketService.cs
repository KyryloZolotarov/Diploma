using Basket.Host.Models;

namespace Basket.Host.Services.Interfaces;

public interface IBasketService
{
    Task Add(string basketId, int itemId);
    Task AddItems(string basketId, BasketItem item);
    Task<BasketItemsDb> ChangeItemsCount(string basketId, BasketItem item);
    Task<BasketItemsDb> Get(string basketId);
    Task Delete(string basketId);
    Task<BasketItemsDb> DeleteItem(string basketId, int itemId);
}