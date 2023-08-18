using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Data.Entities
{
    public class CatalogModel
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(100)]
        [Required]
        public string Model { get; set; }
        [Required]
        public int CatalogBrandId { get; set; }

        public CatalogBrand CatalogBrand { get; set; }
    }
}
