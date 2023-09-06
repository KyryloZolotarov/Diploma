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

        public Task<int?> Add(int userId)
        {
            return ExecuteSafeAsync(() => _orderOrderRepository.Add(userId));
        }

        public Task<int?> Update(int userId)
        {
            return ExecuteSafeAsync(() => _orderOrderRepository.Update(userId));
        }

        public Task<int?> Delete(int id)
        {
            return ExecuteSafeAsync(() => _orderOrderRepository.Delete(id));
        }
    }
}
