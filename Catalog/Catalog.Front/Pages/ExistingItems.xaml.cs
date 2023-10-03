using Catalog.Front.Services.Interfaces;
using Catalog.Front.Models;
using System.Collections.ObjectModel;
using Catalog.Front.ViewModels;

namespace Catalog.Front.Pages;

public partial class ExistingItems : ContentPage
{
    private readonly ICatalogItemService _catalogItemService;
    public ObservableCollection<CatalogItemDto> Items { get; set; }
    public ExistingItems(ICatalogItemService catalogItemService)
    {
        InitializeComponent();
        _catalogItemService = catalogItemService;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Items = _catalogItemService.GetItemsAsync().GetAwaiter().GetResult();
        catalogItemsList.ItemsSource = Items;
    }

    private void OnLabelClicked(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync(nameof(SinglItem));
    }
}