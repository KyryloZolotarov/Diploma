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

        public async Task<string> Add(string id, string name, string givenName, string familyName, string email, string address)
        {
            return await ExecuteSafeAsync(() => _orderUserRepository.Add(id, name, givenName,  familyName,  email,  address));
        }

        public async Task<string> Update(string id, string name, string givenName, string familyName, string email, string address)
        {
            return await ExecuteSafeAsync(() => _orderUserRepository.Update(id, name, givenName, familyName, email, address));
        }

        public async Task<string> Delete(string id)
        {
            return await ExecuteSafeAsync(() => _orderUserRepository.Delete(id));
        }
    }
}
