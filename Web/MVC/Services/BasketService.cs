using MVC.Services.Interfaces;
using MVC.ViewModels;
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

        public async Task<IEnumerable<Basket>> GetBasket()
        {
            var result = await _httpClient.SendAsync<IEnumerable<Basket>>($"{_settings.Value.BasketUrl}/GetBasket", HttpMethod.Get);
            return result;
        }

        public Task<Basket> AddItemToBasket(int? itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Basket> DeleteItemFromBasket(int? itemId)
        {
            throw new NotImplementedException();
        }

        public Task<Basket> ClearBasket()
        {
            throw new NotImplementedException();
        }
    }
}
