using Infrastructure.Services.Interfaces;
using MVC.Repositories.Interfaces;
using MVC.ViewModels.BasketViewModels;

namespace MVC.Repositories
{
    public class BasketRepository : IBasketRepository
    {
        private readonly IHttpClientService _httpClient;
        private readonly IOptions<AppSettings> _settings;

        public BasketRepository(IHttpClientService httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }
        public async Task<BasketItemsFromBasket> GetBasket()
        {
            return await _httpClient.SendAsync<BasketItemsFromBasket>($"{_settings.Value.BasketUrl}/Get", HttpMethod.Get);
        }

        public async Task AddItemToBasket(int itemId)
        {
            await _httpClient.SendAsync($"{_settings.Value.BasketUrl}/AddItem", HttpMethod.Post, itemId);
        }

        public async Task<BasketItemsFromBasket> ChangeItemsCountInBasket(int itemId, int itemsCount)
        {
            return await _httpClient.SendAsync<BasketItemsFromBasket, BasketItem>(
            $"{_settings.Value.BasketUrl}/ChangeItemsCount",
            HttpMethod.Post,
            new BasketItem
            {
                Id = itemId,
                Count = itemsCount
            });
        }

        public async Task AddItemsInBasket(int itemId, int itemsCount)
        {
            await _httpClient.SendAsync($"{_settings.Value.BasketUrl}/AddItems", HttpMethod.Post,
            new BasketItem { Id = itemId, Count = itemsCount });
        }

        public async Task<BasketItemsFromBasket> DeleteItemFromBasket(int id)
        {
            return await _httpClient.SendAsync<BasketItemsFromBasket, int>($"{_settings.Value.BasketUrl}/DeleteItem",
                HttpMethod.Post, id);
        }
        public async Task ClearBasket()
        {
            await _httpClient.SendAsync($"{_settings.Value.BasketUrl}/Delete", HttpMethod.Delete);
        }
    }
}
