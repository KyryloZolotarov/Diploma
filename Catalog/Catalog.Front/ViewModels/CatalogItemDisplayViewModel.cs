using Catalog.Front.Models;
using Catalog.Front.Services.Interfaces;
using System.Collections.ObjectModel;
using System.ComponentModel;

namespace Catalog.Front.ViewModels;

public class CatalogItemDisplayViewModel
{
    public ObservableCollection<CatalogItemDto> Items { get; set; }
    private readonly ICatalogItemService _catalogItemService;
    public CatalogItemDisplayViewModel(ICatalogItemService catalogItemService)
    {
        _catalogItemService = catalogItemService;
        Items = new ObservableCollection<CatalogItemDto>();
    }
    public async Task GetItemsAsync()
    {
        var items = await _catalogItemService.GetItemsAsync();
        if (items != null)
        {
            foreach (var item in items)
            {
                Items.Add(item);
            }
        }
        else
        {
            Items.Add(new CatalogItemDto()
            {
                Id = 0,
                Name = "Name",
                Description = "Description",
                Price = 0,
                AvailableStock = 0
            });
            Items.Add(new CatalogItemDto()
            {
                Id = 0,
                Name = "Name",
                Description = "Description",
                Price = 0,
                AvailableStock = 0
            });
        }
    }
}