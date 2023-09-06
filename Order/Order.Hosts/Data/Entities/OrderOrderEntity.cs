namespace Order.Hosts.Data.Entities
{
    public class OrderOrderEntity
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public OrderUserEntity User { get; set; }
    }
}
