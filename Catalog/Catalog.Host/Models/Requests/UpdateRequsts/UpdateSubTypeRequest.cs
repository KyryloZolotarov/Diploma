namespace Catalog.Host.Models.Requests.UpdateRequsts
{
    public class UpdateSubTypeRequest
    {
        public int Id { get; set; }

        public string SubTypeName { get; set; } = string.Empty;

        public int CatalogTypeId { get; set; }
    }
}
