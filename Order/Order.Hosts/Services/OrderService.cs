using Order.Host.Data;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Responses;
using Order.Hosts.Repositories.Interfaces;
using Order.Hosts.Services.Interfaces;

namespace Order.Hosts.Services
{
    public class OrderService : BaseDataService<ApplicationDbContext>, IOrderService
    {
        private readonly IOrderOrderRepository _orderOrderRepository;
        private readonly IMapper _mapper;

        public OrderService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IOrderOrderRepository orderOrderRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _orderOrderRepository = orderOrderRepository;
            _mapper = mapper;
        }

        public async Task<bool> AddOrder(OrderUserDto user, OrderItemDto orderAdding)
        {
            return await ExecuteSafeAsync(() => _orderOrderRepository.AddOrder(user, orderAdding));
        }

        public async Task<OrderItemDto> GetOrder(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _orderOrderRepository.GetOrder(id);
                return _mapper.Map<OrderItemDto>(result);
            });
        }

        public async Task<ListOrdersResponse> GetOrderList(string userId)
        {
            throw new NotImplementedException();
        }
    }
}
