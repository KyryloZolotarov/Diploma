namespace Order.Hosts.Data.Entities
{
    public class OrderOrderEntity
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
        public OrderUserEntity User { get; set; }
    }
}
