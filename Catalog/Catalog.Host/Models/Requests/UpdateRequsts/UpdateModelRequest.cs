namespace Catalog.Host.Models.Requests.UpdateRequsts
{
    public class UpdateModelRequest
    {
        public int Id { get; set; }

        public string ModelName { get; set; } = string.Empty;

        public int CatalogBrandId { get; set; }
    }
}
