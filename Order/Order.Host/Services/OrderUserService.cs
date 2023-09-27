using Infrastructure.Exceptions;
using Order.Host.Data;
using Order.Hosts.Data.Entities;
using Order.Hosts.Repositories;
using Order.Hosts.Repositories.Interfaces;
using Order.Hosts.Services.Interfaces;

namespace Order.Hosts.Services;

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
        var user = new OrderUserEntity() { Id = id, Name = name, GivenName = givenName, FamilyName = familyName, Email = email, Address = address };
        return await ExecuteSafeAsync(() => _orderUserRepository.Add(user));
    }

    public async Task<string> Update(string id, string name, string givenName, string familyName, string email, string address)
    {
        var userExists = await ExecuteSafeAsync(() => _orderUserRepository.CheckUserExist(id));
        if (userExists == null)
        {
            throw new BusinessException($"User with id: {id} not found");
        }

        userExists.Id = id;
        userExists.Name = name;
        userExists.GivenName = givenName;
        userExists.FamilyName = familyName;
        userExists.Email = email;
        userExists.Address = address;

        return await ExecuteSafeAsync(() => _orderUserRepository.Update(userExists));
    }

    public async Task<string> Delete(string id)
    {
        var user = await ExecuteSafeAsync(() => _orderUserRepository.CheckUserExist(id));
        if (user == null)
        {
            throw new BusinessException($"User with id: {id} not found");
        }

        return await ExecuteSafeAsync(() => _orderUserRepository.Delete(user));
    }
}