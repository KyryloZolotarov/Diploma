using Basket.Host.Models;
using Basket.Host.Services.Interfaces;
using Microsoft.Extensions.Logging;

namespace Basket.Host.Services
{
    public class BasketService : IBasketService
    {
        private readonly ICacheService _cacheService;
        private readonly ILogger<BasketService> _logger;

        public BasketService(ILogger<BasketService> logger, ICacheService cacheService)
        {
            _cacheService = cacheService;
            _logger = logger;
        }

        public async Task Add(string basketId, int itemId)
        {
            var item = new BasketItem() { Id = itemId, Count = 1 };
            await _cacheService.AddOrUpdateAsync(basketId, item);
        }

        public async  Task AddItems(string basketId, BasketItem item)
        {
            await _cacheService.AddOrUpdateAsync(basketId, item);
        }

        public async Task<BasketItemsDb> ChangeItemsCount(string basketId, BasketItem item)
        {
            var basketItems = await _cacheService.GetAsync<BasketItemsDb>(basketId);
            if (basketItems.Items != null)
            {
                var deletingItem = basketItems.Items.Find(x => x.Id == item.Id);
                if (deletingItem != null) basketItems.Items.Remove(deletingItem);
            }

            if (basketItems.Items != null) basketItems.Items.Add(item);
            await _cacheService.AddOrUpdateAsync(basketId, basketItems);
            var basket = await _cacheService.GetAsync<BasketItemsDb>(basketId);
            return basket;
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

        public async Task<BasketItemsDb> DeleteItem(string basketId, int itemId)
        {
            var basketItems = await _cacheService.GetAsync<BasketItemsDb>(basketId);
            if (basketItems.Items != null)
            {
                var deletingItem = basketItems.Items.Find(x => x.Id == itemId);
                if (deletingItem != null) basketItems.Items.Remove(deletingItem);
            }
            await _cacheService.AddOrUpdateAsync(basketId, basketItems);
            var basket = await _cacheService.GetAsync<BasketItemsDb>(basketId);
            return basket;
        }
    }
}
