using Order.Hosts.Data.Entities;

namespace Order.Hosts.Models.BaseResponses;

public class OrderOrderResponse
{
    public List<OrderItemEntity> Items { get; set; }
    public OrderOrderEntity Order { get; set; } = new ();
}