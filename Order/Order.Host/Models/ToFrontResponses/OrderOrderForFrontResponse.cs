using Order.Hosts.Models.Dtos;

namespace Order.Hosts.Models.ToFrontResponses;

public class OrderOrderForFrontResponse
{
    public List<OrderItemDto> Items { get; set; }
    public OrderOrderDto Order { get; set; }
}