namespace Order.Hosts.Models.Requests;

public class UpdateOrderRequest
{
    public int Id { get; set; }
    public string UserId { get; set; }
    public DateTime DateTime { get; set; }
}