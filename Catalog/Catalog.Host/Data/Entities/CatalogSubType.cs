namespace Catalog.Host.Data.Entities
{
    public class CatalogSubType
    {
        public int Id { get; set; }

        public string SubType { get; set; }

        public int CatalogTypeId { get; set; }

        public CatalogType CatalogType { get; set; }
    }
}
