using MVC.Services.Interfaces;
using MVC.ViewModels;
using MVC.ViewModels.BasketViewModels;
using MVC.ViewModels.CatalogViewModels;

namespace MVC.Services
{
    public class BasketService : IBasketService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IHttpClientService _httpClient;
        private readonly ILogger<CatalogService> _logger;

        public BasketService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
        }

        public async Task<IEnumerable<BasketItemFromCatalog>> GetBasket()
        {
            var itemsListId = await _httpClient.SendAsync<IEnumerable<BasketItemsResponse>>($"{_settings.Value.BasketUrl}/Get", HttpMethod.Get);
            var result =
                await _httpClient.SendAsync<IEnumerable<BasketItemFromCatalog>, IEnumerable<BasketItemsResponse>>(
                    $"{_settings.Value.CatalogUrl}/ListItems", HttpMethod.Get, itemsListId);
            return result;
        }

        public async Task AddItemToBasket(int? itemId)
        {
            await _httpClient.SendAsync($"{_settings.Value.BasketUrl}/Add", HttpMethod.Post, itemId.ToString());
        }

        public async Task<IEnumerable<BasketItemFromCatalog>> DeleteItemFromBasket(int? itemId)
        {

            var itemsListId = await _httpClient.SendAsync<IEnumerable<BasketItemsResponse>, string>($"{_settings.Value.BasketUrl}/Delete", HttpMethod.Delete, itemId.ToString());
            var result =
                await _httpClient.SendAsync<IEnumerable<BasketItemFromCatalog>, IEnumerable<BasketItemsResponse>>(
                    $"{_settings.Value.CatalogUrl}/ListItems", HttpMethod.Get, itemsListId);
            return result;
        }

        public async Task ClearBasket()
        {
            await _httpClient.SendAsync($"{_settings.Value.BasketUrl}/DeleteItem", HttpMethod.Delete);
        }
    }
}
