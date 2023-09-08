using Order.Hosts.Models.BaseResponses;
using Order.Hosts.Models.Dtos;

namespace Order.Hosts.Models.ToFrontResponses
{
    public class ListOrderForFrontResponse
    {
        public List<OrderOrderDto> Orders { get; set; }
    }
}
