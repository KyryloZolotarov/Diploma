namespace MVC.Models.Responses;

public class ListOrderItemsResponse
{
    public List<OrderItemResponse> Items { get; set; }
    public OrderFromDb Order { get; set; }
}