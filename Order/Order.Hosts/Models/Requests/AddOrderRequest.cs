using Order.Hosts.Models.Dtos;

namespace Order.Hosts.Models.Requests
{
    public class AddOrderRequest
    {
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
    }
}
