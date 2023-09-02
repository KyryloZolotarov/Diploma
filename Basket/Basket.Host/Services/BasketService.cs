using Basket.Host.Models;
using Basket.Host.Services.Interfaces;

namespace Basket.Host.Services
{
    public class BasketService : IBasketService
    {
        private readonly ICacheService _cacheService;

        public BasketService(ICacheService cacheService)
        {
            _cacheService = cacheService;
        }

        public async Task Add(string basketId, string itemId)
        {
            await _cacheService.AddOrUpdateAsync(basketId, itemId);
        }

        public async Task<BasketItemsDb> Get(string basketId)
        {
            var result = await _cacheService.GetAsync<BasketItemsDb>(basketId);
            return result;
        }

        public Task Delete(string basketId)
        {
            return _cacheService.Delete(basketId);
        }

        public async Task<BasketItemsDb> DeleteItem(string basketId, string itemId)
        {
            var basketItems = await _cacheService.GetAsync<BasketItemsDb>(basketId);
            BasketItem item = new BasketItem() { Id = itemId };
            if (basketItems.Items != null) basketItems.Items.Remove(item);
            await _cacheService.AddOrUpdateAsync(basketId, basketItems);
            var basket = await _cacheService.GetAsync <BasketItemsDb> (basketId);
            return basket;
        }
    }
}
