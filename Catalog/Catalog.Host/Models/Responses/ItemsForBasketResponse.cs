namespace Catalog.Host.Models.Responses;

public class ItemsForBasketResponse<T>
{
    public IEnumerable<T> Data { get; init; } = null!;
}