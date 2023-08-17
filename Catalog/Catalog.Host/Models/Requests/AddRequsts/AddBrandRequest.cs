using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.AddRequsts
{
    public class AddBrandRequest
    {
        [Required]
        public int Id { get; set; }
        [Required]
        [StringLength(20)]
        public string BrandName { get; set; } = string.Empty;
    }
}
