using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Models.Requests.AddRequsts
{
    public class AddSubTypeRequest
    {
        [Required]
        [MaxLength(30)]
        public string SubTypeName { get; set; } = string.Empty;
        [Required]
        public int CatalogTypeId { get; set; }
    }
}
