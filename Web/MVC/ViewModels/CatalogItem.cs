namespace MVC.ViewModels;

public record CatalogItem
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string PictureUrl { get; set; }

    public CatalogSubType CatalogSubType { get; set; }

    public CatalogModel CatalogModel { get; set; }

    public int AvailableStock { get; set; }
}