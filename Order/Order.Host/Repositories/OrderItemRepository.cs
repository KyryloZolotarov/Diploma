using Infrastructure.Exceptions;
using Order.Host.Data;
using Order.Hosts.Data.Entities;
using Order.Hosts.Repositories.Interfaces;

namespace Order.Hosts.Repositories;

public class OrderItemRepository : IOrderItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<OrderItemRepository> _logger;

    public OrderItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<OrderItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> Add(OrderItemEntity item)
    {
        await _dbContext.OrderItems.AddAsync(item);
        await _dbContext.SaveChangesAsync();
        return item.Id;
    }

    public async Task<int?> Update(OrderItemEntity item)
    {
        _dbContext.OrderItems.Update(item);
        await _dbContext.SaveChangesAsync();
        return item.Id;
    }

    public async Task<int?> Delete(OrderItemEntity item)
    {
        _dbContext.OrderItems.Remove(item);
        await _dbContext.SaveChangesAsync();
        return item.Id;
    }

    public async Task<OrderItemEntity> CheckItemExist(int id)
    {
        return await _dbContext.OrderItems.FirstOrDefaultAsync(h => h.Id == id);
    }
}