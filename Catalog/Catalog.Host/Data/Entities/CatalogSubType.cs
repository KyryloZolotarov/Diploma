using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Data.Entities
{
    public class CatalogSubType
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string SubType { get; set; }
        [Required]
        public int CatalogTypeId { get; set; }

        public CatalogType CatalogType { get; set; }
    }
}
