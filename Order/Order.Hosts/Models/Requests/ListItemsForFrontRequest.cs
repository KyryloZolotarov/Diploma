using Order.Hosts.Models.Dtos;

namespace Order.Hosts.Models.Requests
{
    public class ListItemsForFrontRequest
    {
        public List<OrderItemDto> Items { get; set; }

        public DateTime DateTime { get; set; }
    }
}
