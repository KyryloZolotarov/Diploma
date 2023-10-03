using Catalog.Front.ViewModels;
using Catalog.Front.Services.Interfaces;

namespace Catalog.Front.Pages;

public partial class AddItem : ContentPage
{
	private CatalogItemAddViewModel _viewModel;
	public AddItem(ICatalogItemService catalogItemService)
	{
        InitializeComponent();
        _viewModel = new CatalogItemAddViewModel(catalogItemService);
        BindingContext = _viewModel;
        AddItemAsync().GetAwaiter();
    }
    public async Task AddItemAsync()
    {

    }
}