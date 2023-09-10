using MVC.Services.Interfaces;
using MVC.ViewModels.BasketViewModels;

namespace MVC.Services;

public class BasketService : IBasketService
{
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<BasketService> _logger;
    private readonly IOptions<AppSettings> _settings;

    public BasketService(IHttpClientService httpClient, ILogger<BasketService> logger, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
    }

    public async Task<BasketIndexViewModel> GetBasket()
    {
        var itemsListId =
            await _httpClient.SendAsync<BasketItemsFromBasket>($"{_settings.Value.BasketUrl}/Get", HttpMethod.Get);
        if (itemsListId == null) return new BasketIndexViewModel();

        foreach (var item in itemsListId.Items) _logger.LogInformation($"Item id from basket {item.Id}");

        var result =
            await _httpClient.SendAsync<BasketItemsFromCatalog, BasketItemsFromBasket>(
                $"{_settings.Value.CatalogUrl}/ListItems", HttpMethod.Post, itemsListId);
        var itemForDisplay = new BasketIndexViewModel
        {
            BasketItems = itemsListId.Items
                .Select(item => new BasketItemForDisplay { Id = item.Id, Count = item.Count }).ToList()
        };
        if (itemForDisplay == null || itemForDisplay.BasketItems == null) return new BasketIndexViewModel();
        foreach (var item in itemForDisplay.BasketItems)
        {
            _logger.LogInformation($"Basket item mapping id: {item.Id}");
            if (item == null || item.Id == null) break;
            var temp = result.Items.FirstOrDefault(x => x.Id == item.Id);
            if (temp == null || temp.Id == null) break;

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

    public async Task<BasketIndexViewModel> ChangeItemsCountInBasket(int itemId, int itemsCount)
    {
        var itemsListId = await _httpClient.SendAsync<BasketItemsFromBasket, BasketItem>(
            $"{_settings.Value.BasketUrl}/ChangeItemsCount",
            HttpMethod.Post,
            new BasketItem
            {
                Id = itemId,
                Count = itemsCount
            });
        if (itemsListId == null) return new BasketIndexViewModel();

        foreach (var item in itemsListId.Items) _logger.LogInformation($"Item id from basket {item.Id}");

        var result =
            await _httpClient.SendAsync<BasketItemsFromCatalog, BasketItemsFromBasket>(
                $"{_settings.Value.CatalogUrl}/ListItems", HttpMethod.Post, itemsListId);
        var itemForDisplay = new BasketIndexViewModel
        {
            BasketItems = itemsListId.Items
                .Select(item => new BasketItemForDisplay { Id = item.Id, Count = item.Count }).ToList()
        };
        if (itemForDisplay == null || itemForDisplay.BasketItems == null) return new BasketIndexViewModel();
        foreach (var item in itemForDisplay.BasketItems)
        {
            _logger.LogInformation($"Basket item mapping id: {item.Id}");
            if (item == null || item.Id == null) break;
            var temp = result.Items.FirstOrDefault(x => x.Id == item.Id);
            if (temp == null || temp.Id == null) break;

            item.CatalogModel = temp.CatalogModel;
            item.CatalogSubType = temp.CatalogSubType;
            item.Name = temp.Name;
            item.Price = temp.Price;
        }

        return itemForDisplay;
    }

    public async Task AddItemsInBasket(int itemId, int itemsCount)
    {
        await _httpClient.SendAsync($"{_settings.Value.BasketUrl}/AddItems", HttpMethod.Post,
            new BasketItem { Id = itemId, Count = itemsCount });
    }

    public async Task<BasketIndexViewModel> DeleteItemFromBasket(int id)
    {
        var itemsListId =
            await _httpClient.SendAsync<BasketItemsFromBasket, int>($"{_settings.Value.BasketUrl}/DeleteItem",
                HttpMethod.Post, id);
        if (itemsListId == null) return new BasketIndexViewModel();

        foreach (var item in itemsListId.Items) _logger.LogInformation($"Item id from basket {item.Id}");

        var result =
            await _httpClient.SendAsync<BasketItemsFromCatalog, BasketItemsFromBasket>(
                $"{_settings.Value.CatalogUrl}/ListItems", HttpMethod.Post, itemsListId);
        var itemForDisplay = new BasketIndexViewModel
        {
            BasketItems = itemsListId.Items
                .Select(item => new BasketItemForDisplay { Id = item.Id, Count = item.Count }).ToList()
        };
        if (itemForDisplay == null || itemForDisplay.BasketItems == null) return new BasketIndexViewModel();
        foreach (var item in itemForDisplay.BasketItems)
        {
            _logger.LogInformation($"Basket item mapping id: {item.Id}");
            if (item == null || item.Id == null) break;
            var temp = result.Items.FirstOrDefault(x => x.Id == item.Id);
            if (temp == null || temp.Id == null) break;

            item.CatalogModel = temp.CatalogModel;
            item.CatalogSubType = temp.CatalogSubType;
            item.Name = temp.Name;
            item.Price = temp.Price;
        }

        return itemForDisplay;
    }

    public async Task ClearBasket()
    {
        await _httpClient.SendAsync($"{_settings.Value.BasketUrl}/Delete", HttpMethod.Delete);
    }
}