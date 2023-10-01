using Infrastructure.Services.Interfaces;
using MVC.Repositories.Interfaces;
using MVC.Services.Interfaces;
using MVC.ViewModels.BasketViewModels;

namespace MVC.Services;

public class BasketService : IBasketService
{
    private readonly IBasketRepository _basketRepository;
    private readonly ICatalogRepository _catalogRepository;
    private readonly ILogger<BasketService> _logger;

    public BasketService(ILogger<BasketService> logger, IBasketRepository basket, ICatalogRepository catalog)
    {
        _catalogRepository = catalog;
        _basketRepository = basket;
        _logger = logger;
    }

    public async Task<BasketIndexViewModel> GetBasket()
    {
        var itemsListId = await _basketRepository.GetBasket();
        if (itemsListId == null) return new BasketIndexViewModel();

        foreach (var item in itemsListId.Items) _logger.LogInformation($"Item id from basket {item.Id}");

        var result = await _catalogRepository.GetCatalogItemsForBasket(itemsListId);
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
        await _basketRepository.AddItemToBasket(itemId);
    }

    public async Task<BasketIndexViewModel> ChangeItemsCountInBasket(int itemId, int itemsCount)
    {
        var itemsListId = await _basketRepository.ChangeItemsCountInBasket(itemId, itemsCount);
        if (itemsListId == null) return new BasketIndexViewModel();

        foreach (var item in itemsListId.Items) _logger.LogInformation($"Item id from basket {item.Id}");

        var result = await _catalogRepository.GetCatalogItemsForBasket(itemsListId);
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
        await _basketRepository.AddItemsInBasket(itemId, itemsCount);
    }

    public async Task<BasketIndexViewModel> DeleteItemFromBasket(int id)
    {
        var itemsListId = await _basketRepository.DeleteItemFromBasket(id);
        if (itemsListId == null) return new BasketIndexViewModel();

        foreach (var item in itemsListId.Items) _logger.LogInformation($"Item id from basket {item.Id}");

        var result = await _catalogRepository.GetCatalogItemsForBasket(itemsListId);
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
        await _basketRepository.ClearBasket();
    }
}