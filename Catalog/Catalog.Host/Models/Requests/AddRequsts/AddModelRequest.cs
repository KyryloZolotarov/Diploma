using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.AddRequsts;

public class AddModelRequest
{
    [Required] [MaxLength(40)] public string ModelName { get; set; } = string.Empty;

    [Required] public int CatalogBrandId { get; set; }
}