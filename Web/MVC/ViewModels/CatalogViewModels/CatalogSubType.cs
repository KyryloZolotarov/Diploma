namespace MVC.ViewModels.CatalogViewModels;

public record CatalogSubType
{
    public int Id { get; set; }

    public string SubType { get; set; }

    public CatalogType CatalogType { get; set; }
}