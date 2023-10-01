using Catalog.Host.Models.Dtos;

namespace Catalog.Host.Models.Requests;

public class ItemsForBasketRequest
{
    public List<BasketItemDto> Items { get; set; }
}