using Order.Hosts.Models.Dtos;

namespace Order.Hosts.Models.Requests
{
    public class OrderOrderUserFromMVC
    {
        public List<AddOrderFromMVCRequest> OrderItems { get; set; }
        public OrderUserDto User { get; set; }
    }
}
