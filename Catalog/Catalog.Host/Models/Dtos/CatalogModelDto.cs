namespace Catalog.Host.Models.Dtos;

public class CatalogModelDto
{
    public int Id { get; set; }

    public string Model { get; set; }

    public CatalogBrandDto CatalogBrand { get; set; }
}