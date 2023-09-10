using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.AddRequsts;

public class AddItemRequest
{
    [Required] [MaxLength(20)] public string Name { get; set; } = null!;

    [MaxLength(20)] public string PartNumber { get; set; } = null!;

    [MaxLength(300)] public string Description { get; set; } = null!;

    [Range(0.1, 10000)] public decimal Price { get; set; }

    public string PictureFileName { get; set; } = null!;

    [Required] public int CatalogSubTypeId { get; set; }

    [Required] public int CatalogModelId { get; set; }

    [Range(0, 200)] public int AvailableStock { get; set; }
}