using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.UpdateRequsts;

public class UpdateBrandRequest
{
    [Required] public int Id { get; set; }

    [Required] [MaxLength(30)] public string BrandName { get; set; } = string.Empty;
}