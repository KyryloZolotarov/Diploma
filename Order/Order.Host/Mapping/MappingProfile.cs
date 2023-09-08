using Order.Hosts.Data.Entities;
using Order.Hosts.Models.Dtos;

namespace Order.Hosts.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderItemEntity, OrderItemDto>();
            CreateMap<OrderOrderEntity, OrderOrderDto>();
            CreateMap<OrderUserEntity, OrderUserDto>();
        }
    }
}
