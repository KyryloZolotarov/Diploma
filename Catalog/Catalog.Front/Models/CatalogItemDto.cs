namespace Catalog.Front.Models.Dtos;

public class CatalogItemDto
{
    public int Id { get; set; }

    public string Name { get; set; }

    public string Description { get; set; }

    public decimal Price { get; set; }

    public string PictureUrl { get; set; }

    public CatalogSubTypeDto CatalogSubType { get; set; }

    public CatalogModelDto CatalogModel { get; set; }

    public int AvailableStock { get; set; }
}