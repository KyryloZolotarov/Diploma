namespace Catalog.Host.Models.Requests.UpdateRequsts
{
    public class UpdateItemRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;

        public string PartNumber { get; set; } = null!;

        public string Description { get; set; } = null!;

        public decimal Price { get; set; }

        public string PictureFileName { get; set; } = null!;

        public int CatalogSubTypeId { get; set; }

        public int CatalogModelId { get; set; }

        public int AvailableStock { get; set; }
    }
}
