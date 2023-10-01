using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.AddRequsts;

public class AddBrandRequest
{
    [Required] [MaxLength(30)] public string BrandName { get; set; } = string.Empty;
}