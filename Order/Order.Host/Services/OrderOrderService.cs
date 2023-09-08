using Order.Host.Data;
using Order.Hosts.Repositories;
using Order.Hosts.Repositories.Interfaces;
using Order.Hosts.Services.Interfaces;

namespace Order.Hosts.Services
{
    public class OrderOrderService : BaseDataService<ApplicationDbContext>, IOrderOrderService
    {
        private readonly IOrderOrderRepository _orderOrderRepository;

        public OrderOrderService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IOrderOrderRepository orderOrderRepository)
            : base(dbContextWrapper, logger)
        {
            _orderOrderRepository = orderOrderRepository;
        }

        public async Task<int?> Add(string userId, DateTime dateTime)
        {
            return await ExecuteSafeAsync(() => _orderOrderRepository.Add(userId, dateTime));
        }

        public async Task<int?> Update(string userId, DateTime dateTime)
        {
            return await ExecuteSafeAsync(() => _orderOrderRepository.Update(userId, dateTime));
        }

        public async Task<int?> Delete(int id)
        {
            return await ExecuteSafeAsync(() => _orderOrderRepository.Delete(id));
        }
    }
}
