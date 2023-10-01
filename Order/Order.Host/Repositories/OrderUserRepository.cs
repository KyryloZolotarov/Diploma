using Infrastructure.Exceptions;
using Order.Host.Data;
using Order.Hosts.Data.Entities;
using Order.Hosts.Repositories.Interfaces;

namespace Order.Hosts.Repositories;

public class OrderUserRepository : IOrderUserRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<OrderUserRepository> _logger;

    public OrderUserRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<OrderUserRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<string> Add(OrderUserEntity user)
    {
        await _dbContext.OrderUsers.AddAsync(user);
        await _dbContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<string> Update(OrderUserEntity user)
    {
        _dbContext.OrderUsers.Update(user);
        await _dbContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<string> Delete(OrderUserEntity user)
    {
        _dbContext.OrderUsers.Remove(user);
        await _dbContext.SaveChangesAsync();
        return user.Id;
    }

    public async Task<OrderUserEntity> CheckUserExist(string id)
    {
        return await _dbContext.OrderUsers.FirstOrDefaultAsync(h => h.Id == id);
    }
}