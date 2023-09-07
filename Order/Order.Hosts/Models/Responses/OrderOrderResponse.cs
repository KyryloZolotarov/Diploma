using Order.Hosts.Data.Entities;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;

namespace Order.Hosts.Models.Responses
{
    public class OrderOrderResponse
    {
        public List<OrderItemEntity> Items { get; set; }
        public OrderOrderDto Order { get; set; }
    }
}
