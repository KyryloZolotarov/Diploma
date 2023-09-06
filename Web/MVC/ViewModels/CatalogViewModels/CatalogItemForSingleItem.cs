using MVC.ViewModels.BasketViewModels;

namespace MVC.ViewModels.CatalogViewModels
{
    public class CatalogItemForSingleItem
    {
        public CatalogItem catalogItem { get; set; }
        public BasketItem basketItem { get; set; }
    }
}
