namespace Order.Hosts.Models.Responses;

public class BaseResponse<T>
{
    public T Id { get; set; } = default!;
}