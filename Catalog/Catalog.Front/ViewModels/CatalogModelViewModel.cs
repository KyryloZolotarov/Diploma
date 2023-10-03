namespace Catalog.Front.ViewModels;

public class CatalogModelViewModel
{
    public int Id { get; set; }

    public string Model { get; set; }

    public CatalogBrandViewModel CatalogBrand { get; set; }
}