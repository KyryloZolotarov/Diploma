using Order.Host.Data;
using Order.Hosts.Repositories;
using Order.Hosts.Repositories.Interfaces;
using Order.Hosts.Services.Interfaces;

namespace Order.Hosts.Services
{
    public class OrderUserService : BaseDataService<ApplicationDbContext>, IOrderUserService
    {
        private readonly IOrderUserRepository _orderUserRepository;

        public OrderUserService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            IOrderUserRepository orderUserRepository)
            : base(dbContextWrapper, logger)
        {
            _orderUserRepository = orderUserRepository;
        }

        public Task<int?> Add(int id, string name, string givenName, string familyName, string email, string address)
        {
            return ExecuteSafeAsync(() => _orderUserRepository.Add(id, name, givenName,  familyName,  email,  address));
        }

        public Task<int?> Update(int id, string name, string givenName, string familyName, string email, string address)
        {
            return ExecuteSafeAsync(() => _orderUserRepository.Update(id, name, givenName, familyName, email, address));
        }

        public Task<int?> Delete(int id)
        {
            return ExecuteSafeAsync(() => _orderUserRepository.Delete(id));
        }
    }
}
