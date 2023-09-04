using Basket.Host.Models;
using Basket.Host.Services.Interfaces;

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
            BasketItemsDb curBasket = new ();
            curBasket = await _cacheService.GetAsync<BasketItemsDb>(basketId);
            curBasket ??= new BasketItemsDb();
            curBasket.Items ??= new List<BasketItem>();
            var foundedItem = curBasket.Items.FirstOrDefault(x => x.Id ==itemId);
            if(foundedItem==null)
            {
                var item = new BasketItem() { Id = itemId, Count = 1 };
                curBasket.Items.Add(item);
                await _cacheService.AddOrUpdateAsync(basketId, curBasket);
            }
            else
            {
                foundedItem.Count++;
                await _cacheService.AddOrUpdateAsync(basketId, curBasket);
            }
            
        }

        public async Task AddItems(string basketId, BasketItem item)
        {
            BasketItemsDb curBasket = new();
            curBasket = await _cacheService.GetAsync<BasketItemsDb>(basketId);
            curBasket ??= new BasketItemsDb();
            curBasket.Items ??= new List<BasketItem>();
            var foundedItem = curBasket.Items.FirstOrDefault(x => x.Id == item.Id);
            if (foundedItem == null)
            {
                curBasket.Items.Add(item);
                await _cacheService.AddOrUpdateAsync(basketId, curBasket);
            }
            else
            {
                foundedItem.Count+= item.Count;
                await _cacheService.AddOrUpdateAsync(basketId, curBasket);
            }
        }

        public async Task<BasketItemsDb> ChangeItemsCount(string basketId, BasketItem item)
        {
            var basket = await _cacheService.GetAsync<BasketItemsDb>(basketId);
            if(basket== null)
            {
                return new BasketItemsDb();
            }
            if (basket.Items != null)
            {
                var foundedItem = basket.Items.FirstOrDefault(x => x.Id == item.Id);
                if(foundedItem!=null)
                {
                    foundedItem.Count = item.Count;
                }
            }

            await _cacheService.AddOrUpdateAsync(basketId, basket);
            return await _cacheService.GetAsync<BasketItemsDb>(basketId);
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
            if (basketItems == null)
            {
                return new BasketItemsDb();
            }
            if (basketItems.Items != null)
            {
                var deletingItem = basketItems.Items.FirstOrDefault(x => x.Id == itemId);
                if (deletingItem != null) basketItems.Items.Remove(deletingItem);
            }
            await _cacheService.AddOrUpdateAsync(basketId, basketItems);
            return await _cacheService.GetAsync<BasketItemsDb>(basketId);
        }
    }
}
