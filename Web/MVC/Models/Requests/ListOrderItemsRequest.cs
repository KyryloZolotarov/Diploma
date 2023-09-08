namespace MVC.Models.Requests
{
    public class ListOrderItemsRequest
    {
        public List<OrderItemRequest> Items { get; set; }
        public DateTime DateTime { get; set; }
    }
}
