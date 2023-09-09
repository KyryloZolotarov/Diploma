using Order.Hosts.Data.Entities;

namespace Order.Hosts.Models.Requests
{
    public class UpdateItemRequest
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CatalogSubTypeId { get; set; }
        public int CatalogModelId { get; set; }
        public int Count { get; set; }
        public int OrderId { get; set; }
    }
}
