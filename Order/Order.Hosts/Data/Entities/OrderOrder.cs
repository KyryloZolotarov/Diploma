namespace Order.Hosts.Data.Entities
{
    public class OrderOrder
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public OrderUser User { get; set; }
    }
}
