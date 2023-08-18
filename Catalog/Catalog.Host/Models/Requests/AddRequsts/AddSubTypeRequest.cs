namespace Catalog.Host.Models.Requests.AddRequsts
{
    public class AddSubTypeRequest
    {
        public string SubTypeName { get; set; } = string.Empty;

        public int CatalogTypeId { get; set; }
    }
}
