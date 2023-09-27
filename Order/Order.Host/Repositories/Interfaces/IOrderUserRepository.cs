using Order.Hosts.Data.Entities;

namespace Order.Hosts.Repositories.Interfaces;

public interface IOrderUserRepository
{
    Task<string> Add(OrderUserEntity user);
    Task<string> Update(OrderUserEntity user);
    Task<string> Delete(OrderUserEntity user);
    Task<OrderUserEntity> CheckUserExist(string id);
}