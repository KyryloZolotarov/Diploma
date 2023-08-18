using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Data.Entities
{
    public class CatalogBrand
    {
        [Key]
        public int Id { get; set; }
        [MaxLength(30)]
        public string Brand { get; set; }
    }
}
