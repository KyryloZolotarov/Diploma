using MVC.ViewModels.CatalogViewModels;

namespace MVC.ViewModels.BasketViewModels
{
    public record BasketItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public CatalogSubType CatalogSubType { get; set; }

        public CatalogModel CatalogModel { get; set; }
    }
}
