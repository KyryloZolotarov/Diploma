namespace Catalog.Host.Models.Requests.AddRequsts
{
    public class AddModelRequest
    {
        public string ModelName { get; set; } = string.Empty;

        public int CatalogBrandId { get; set; }
    }
}
