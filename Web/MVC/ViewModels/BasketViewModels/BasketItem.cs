using MVC.ViewModels.CatalogViewModels;

namespace MVC.ViewModels.BasketViewModels
{
    public record BasketItem
    {
        public int Id { get; set; }
        public int Count { get; set; }
    }
}
