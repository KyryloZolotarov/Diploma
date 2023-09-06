namespace Order.Hosts.Data.Entities
{
    public class OrderItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int CatalogSubTypeId { get; set; }
        public int CatalogModelId { get; set; }
        public int Count { get; set; }
        public OrderOrder Order { get; set; }
    }
}
