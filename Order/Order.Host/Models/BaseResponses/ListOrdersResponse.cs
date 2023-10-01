using Order.Hosts.Data.Entities;

namespace Order.Hosts.Models.BaseResponses;

public class ListOrdersResponse
{
    public List<OrderOrderEntity> Orders { get; set; }
}