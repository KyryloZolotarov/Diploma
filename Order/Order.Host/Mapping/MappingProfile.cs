using Order.Hosts.Data.Entities;
using Order.Hosts.Models.Dtos;

namespace Order.Hosts.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<OrderItemEntity, OrderItemDto>().ForMember(nameof(OrderItemDto.Id), opt
            => opt.MapFrom(x => x.ItemId));
            CreateMap<OrderOrderEntity, OrderOrderDto>();
            CreateMap<OrderUserEntity, OrderUserDto>();
        }
    }
}
