using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.UpdateRequsts;

public class UpdateSubTypeRequest
{
    [Required] public int Id { get; set; }

    [Required] [MaxLength(30)] public string SubTypeName { get; set; } = string.Empty;

    [Required] public int CatalogTypeId { get; set; }
}