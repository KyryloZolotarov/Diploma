using MVC.ViewModels.CatalogViewModels;

namespace MVC.ViewModels.BasketViewModels;

public class BasketItemForDisplay
{
    public int Id { get; set; }

    public string Name { get; set; }

    public decimal Price { get; set; }

    public CatalogSubType CatalogSubType { get; set; }

    public CatalogModel CatalogModel { get; set; }
    public int Count { get; set; }
}