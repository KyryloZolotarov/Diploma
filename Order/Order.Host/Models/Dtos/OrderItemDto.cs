using Order.Hosts.Data.Entities;

namespace Order.Hosts.Models.Dtos;

public class OrderItemDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public decimal Price { get; set; }
    public int CatalogSubTypeId { get; set; }
    public int CatalogModelId { get; set; }
    public int Count { get; set; }
    public int OrderId { get; set; }
    public OrderOrderDto? Order { get; set; }
}