using Catalog.Front.Services.Interfaces;
using Catalog.Front.Models.Dtos;
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
        BindingContext = Items;
    }
    protected override void OnAppearing()
    {
        base.OnAppearing();
        Items = _catalogItemService.GetItemsAsync().GetAwaiter().GetResult();
    }

    private void OnLabelClicked(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync(nameof(SinglItem));
    }
}