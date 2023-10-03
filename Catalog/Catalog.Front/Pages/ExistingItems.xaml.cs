using Catalog.Front.Services.Interfaces;
using Catalog.Front.ViewModels;

namespace Catalog.Front.Pages;

public partial class ExistingItems : ContentPage
{
    private CatalogItemDisplayViewModel _displayViewModel;
    public ExistingItems(ICatalogItemService catalogItemService)
    {
        InitializeComponent();
        _displayViewModel = new CatalogItemDisplayViewModel(catalogItemService);
        BindingContext = _displayViewModel;
        GetItems().GetAwaiter();
    }
    public async Task GetItems()
    {
        await _displayViewModel.GetItemsAsync();
    }

    private void OnLabelClicked(object sender, TappedEventArgs e)
    {
        Shell.Current.GoToAsync(nameof(SinglItem));
    }
}