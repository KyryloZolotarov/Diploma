namespace Catalog.Host.Models.Requests.UpdateRequsts
{
    public class UpdateTypeRequest
    {
        public int Id { get; set; }

        public string TypeName { get; set; } = string.Empty;
    }
}
