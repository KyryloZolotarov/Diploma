using Infrastructure.Exceptions;
using Order.Host.Data;
using Order.Hosts.Data.Entities;
using Order.Hosts.Models.BaseResponses;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;
using Order.Hosts.Repositories.Interfaces;

namespace Order.Hosts.Repositories;

public class OrderOrderRepository : IOrderOrderRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<OrderOrderRepository> _logger;

    public OrderOrderRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<OrderOrderRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> Add(OrderOrderEntity order)
    {
        await _dbContext.OrderOrders.AddAsync(order);
        await _dbContext.SaveChangesAsync();
        return order.Id;
    }

    public async Task<int?> Update(OrderOrderEntity order)
    {
        _dbContext.OrderOrders.Update(order);
        await _dbContext.SaveChangesAsync();
        return order.Id;
    }

    public async Task<int?> Delete(OrderOrderEntity order)
    {
        _dbContext.OrderOrders.Remove(order);
        await _dbContext.SaveChangesAsync();
        return order.Id;
    }

    public async Task<ListOrdersResponse> GetOrderList(string userId)
    {
        var orders = _dbContext.OrderOrders.Where(x => x.UserId == userId).ToList();
        return new ListOrdersResponse { Orders = orders };
    }

    public async Task<OrderOrderEntity> CheckOrderExist(int id)
    {
        return await _dbContext.OrderOrders.FirstOrDefaultAsync(h => h.Id == id);
    }
}