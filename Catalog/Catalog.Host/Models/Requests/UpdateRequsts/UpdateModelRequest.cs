using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.UpdateRequsts;

public class UpdateModelRequest
{
    [Required] public int Id { get; set; }

    [Required] [MaxLength(40)] public string ModelName { get; set; } = string.Empty;

    [Required] public int CatalogBrandId { get; set; }
}