using Order.Hosts.Data.Entities;

namespace Order.Hosts.Models.Dtos
{
    public class OrderOrderDto
    {
        public int Id { get; set; }
        public DateTime DateTime { get; set; }
        public string UserId { get; set; }
        public OrderUserEntity User { get; set; }
    }
}
