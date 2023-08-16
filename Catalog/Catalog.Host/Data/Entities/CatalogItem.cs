namespace Catalog.Host.Data.Entities
{
    public class CatalogItem
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string PictureFileName { get; set; }
        public string PartNumber { get; set; }

        public int CatalogTypeId { get; set; }

        public CatalogType CatalogType { get; set; }

        public int CatalogBrandId { get; set; }

        public CatalogBrand CatalogBrand { get; set; }

        public int CatalogSubTypeId { get; set; }

        public CatalogSubType CatalogSubType { get; set; }

        public int CatalogModelId { get; set; }

        public CatalogModel CatalogModel { get; set; }

        public int AvailableStock { get; set; }
    }
}
