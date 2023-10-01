namespace Catalog.Host.Models.Dtos;

public class CatalogSubTypeDto
{
    public int Id { get; set; }

    public string SubType { get; set; }

    public CatalogTypeDto CatalogType { get; set; }
}