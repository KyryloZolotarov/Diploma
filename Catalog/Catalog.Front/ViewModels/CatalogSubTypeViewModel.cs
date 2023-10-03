namespace Catalog.Front.ViewModels;

public class CatalogSubTypeViewModel
{
    public int Id { get; set; }

    public string SubType { get; set; }

    public CatalogTypeViewModel CatalogType { get; set; }
}