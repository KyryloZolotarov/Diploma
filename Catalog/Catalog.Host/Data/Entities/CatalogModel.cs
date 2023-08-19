using System.ComponentModel.DataAnnotations;

namespace Catalog.Host.Data.Entities
{
    public class CatalogModel
    {
        public int Id { get; set; }
        public string Model { get; set; }
        public int CatalogBrandId { get; set; }

        public CatalogBrand CatalogBrand { get; set; }
    }
}
