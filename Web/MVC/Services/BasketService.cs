using MVC.Services.Interfaces;
using MVC.ViewModels;
using MVC.ViewModels.BasketViewModels;
using MVC.ViewModels.CatalogViewModels;
using BasketItem = MVC.ViewModels.BasketViewModels.BasketItem;

namespace MVC.Services
{
    public class BasketService : IBasketService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IHttpClientService _httpClient;
        private readonly ILogger<BasketService> _logger;

        public BasketService(IHttpClientService httpClient, ILogger<BasketService> logger, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
        }

        public async Task<BasketIndexViewModel> GetBasket()
        {
            var itemsListId = await _httpClient.SendAsync<BasketItemsFromBasket>($"{_settings.Value.BasketUrl}/Get", HttpMethod.Get);
            if (itemsListId == null)
            {
                return new BasketIndexViewModel();

            }

            var result =
                await _httpClient.SendAsync<BasketItemsFromCatalog, BasketItemsFromBasket>(
                    $"{_settings.Value.CatalogUrl}/ListItems", HttpMethod.Get, itemsListId);
            var itemForDisplay = new BasketIndexViewModel()
            {
                BasketItems = itemsListId.Items
                    .Select(item => new BasketItemForDisplay() { Id = item.Id, Count = item.Count }).ToList()
            };
            foreach (var item in itemForDisplay.BasketItems)
            {
                var temp = result.BasketItems.FirstOrDefault(x => x.Id == item.Id);
                item.CatalogModel = temp.CatalogModel;
                item.CatalogSubType = temp.CatalogSubType;
                item.Name = temp.Name;
                item.Price = temp.Price;

            }
            return itemForDisplay;
        }

        public async Task AddItemToBasket(int itemId)
        {
            _logger.LogInformation($"Item id: {itemId}");
            await _httpClient.SendAsync($"{_settings.Value.BasketUrl}/AddItem", HttpMethod.Post, itemId);
        }

        public async Task<IEnumerable<BasketItemForDisplay>> ChangeItemsCountInBasket(int itemId, int itemsCount)
        {
            var itemsListId = (await _httpClient.SendAsync<IEnumerable<BasketItem>, BasketItem>(
                $"{_settings.Value.BasketUrl}/ChangeItemsCount",
                HttpMethod.Post,
                new BasketItem()
                {
                    Id = itemId,
                    Count = itemsCount
                })).ToList();
            var result =
                (await _httpClient.SendAsync<IEnumerable<BasketItemForDisplay>, IEnumerable<BasketItem>>(
                    $"{_settings.Value.CatalogUrl}/ChangeItemsCount", HttpMethod.Get, itemsListId)).ToList();
            foreach (var item in result)
            {
                item.Count = itemsListId.First(x => x.Id == item.Id).Count;
            }
            return result;
        }

        public async Task AddItemsInBasket(int itemId, int itemsCount)
        {
            await _httpClient.SendAsync($"{_settings.Value.BasketUrl}/AddItems", HttpMethod.Post, new BasketItem() { Id = itemId, Count = itemsCount });
        }

        public async Task<IEnumerable<BasketItemForDisplay>> DeleteItemFromBasket(int? itemId)
        {

            var itemsListId = (await _httpClient.SendAsync<IEnumerable<BasketItem>, string>($"{_settings.Value.BasketUrl}/Delete", HttpMethod.Delete, itemId.ToString())).ToList();
            var result =
                (await _httpClient.SendAsync<IEnumerable<BasketItemForDisplay>, IEnumerable<BasketItem>>(
                    $"{_settings.Value.CatalogUrl}/ListItems", HttpMethod.Get, itemsListId)).ToList();
            foreach (var item in result)
            {
                item.Count = itemsListId.First(x => x.Id == item.Id).Count;
            }
            return result;
        }

        public async Task ClearBasket()
        {
            await _httpClient.SendAsync($"{_settings.Value.BasketUrl}/DeleteItem", HttpMethod.Delete);
        }
    }
}
