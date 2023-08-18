using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Data.Entities
{
    public class CatalogItem
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        [Required]
        public string Name { get; set; }
        [MaxLength(250)]
        public string Description { get; set; }
        [Range(0.1, 100000)]
        public decimal Price { get; set; }
        [MaxLength(100)]
        public string PictureFileName { get; set; }
        [MaxLength(15)]
        public string PartNumber { get; set; }
        [Required]
        public int CatalogSubTypeId { get; set; }

        public CatalogSubType CatalogSubType { get; set; }
        [Required]
        public int CatalogModelId { get; set; }

        public CatalogModel CatalogModel { get; set; }
        [Required]
        public int AvailableStock { get; set; }
    }
}
