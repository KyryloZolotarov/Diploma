namespace Catalog.Host.Models.Requests.AddRequsts
{
    public class AddSubTypeRequest
    {
        public int Id { get; set; }

        public string SubTypeName { get; set; } = string.Empty;

        public int CatalogTypeId { get; set; }
    }
}
