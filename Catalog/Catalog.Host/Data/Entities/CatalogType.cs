using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Data.Entities
{
    public class CatalogType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Type { get; set; }
    }
}
