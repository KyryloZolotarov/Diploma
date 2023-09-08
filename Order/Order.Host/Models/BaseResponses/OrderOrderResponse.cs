using Order.Hosts.Data.Entities;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;

namespace Order.Hosts.Models.BaseResponses
{
    public class OrderOrderResponse
    {
        public List<OrderItemEntity> Items { get; set; }
        public OrderOrderEntity Order { get; set; } = new OrderOrderEntity();
    }
}
