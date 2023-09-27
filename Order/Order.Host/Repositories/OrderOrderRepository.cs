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

    public async Task<int?> Add(string userId, DateTime dateTime)
    {
        var order = await _dbContext.OrderOrders.AddAsync(new OrderOrderEntity
        {
            UserId = userId,
            DateTime = dateTime
        });

        await _dbContext.SaveChangesAsync();
        _logger.LogInformation($"Order {order.Entity.Id}");
        return order.Entity.Id;
    }

    public async Task<int?> Update(string userId, DateTime dateTime)
    {
        var orderExists = await _dbContext.OrderOrders.AnyAsync(x => x.UserId == userId);
        if (orderExists)
        {
            var order = _dbContext.OrderOrders.Update(new OrderOrderEntity
            {
                UserId = userId,
                DateTime = dateTime
            });
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Brand {order.Entity.Id}");
            return order.Entity.Id;
        }

        throw new BusinessException($"Brand id: {userId} was not founded");
    }

    public async Task<int?> Delete(int id)
    {
        var orderExists = await _dbContext.OrderOrders.AnyAsync(x => x.Id == id);
        if (orderExists)
        {
            var orderDelete = await _dbContext.OrderOrders.FirstOrDefaultAsync(h => h.Id == id);
            _dbContext.OrderOrders.Remove(orderDelete);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Order Id Deleted {orderDelete.Id}");
            return orderDelete.Id;
        }

        throw new BusinessException($"Order Id {id} was not founded");
    }

    public async Task<bool> AddOrder(CurrentUser user, ListItemsForFrontRequest order)
    {
        var isUserExist = true;
        var userDb = await _dbContext.OrderUsers.FirstOrDefaultAsync(x => x.Id == user.Id);
        if (userDb == null)
        {
            isUserExist = false;
            userDb = new OrderUserEntity
            {
                Id = user.Id,
                Name = user.Name,
                GivenName = user.GivenName,
                FamilyName = user.FamilyName,
                Email = user.Email,
                Address = user.Address
            };
        }

        var orderAdding = new OrderOrderEntity
        {
            UserId = user.Id,
            DateTime = order.DateTime.ToUniversalTime(),
            User = isUserExist ? null : userDb
        };

        var items = new List<OrderItemEntity>();

        foreach (var item in order.Items)
        {
            items.Add(new OrderItemEntity
            {
                ItemId = item.Id,
                Name = item.Name,
                Price = item.Price,
                CatalogModelId = item.CatalogModelId,
                CatalogSubTypeId = item.CatalogSubTypeId,
                OrderId = orderAdding.Id,
                Order = orderAdding
            });
        }

        await _dbContext.OrderItems.AddRangeAsync(items);
        await _dbContext.SaveChangesAsync();

        _logger.LogInformation($"Order id {orderAdding.Id} added");
        return true;
    }

    public async Task<OrderOrderResponse> GetOrder(int id)
    {
        var orderExists = await _dbContext.OrderOrders.AnyAsync(x => x.Id == id);
        if (orderExists)
        {
            var items = _dbContext.OrderItems.Where(x => x.OrderId == id).ToList();
            var order = await _dbContext.OrderOrders.FirstOrDefaultAsync(y => y.Id == id);
            var itemsList = new OrderOrderResponse
            {
                Items = new List<OrderItemEntity>()
            };
            itemsList.Items.AddRange(items);
            itemsList.Order.Id = order.Id;
            itemsList.Order.DateTime = order.DateTime;
            itemsList.Order.UserId = order.UserId;
            itemsList.Order.User = order.User;
            return itemsList;
        }

        throw new BusinessException($"Order with id: {id} not found");
    }

    public async Task<ListOrdersResponse> GetOrderList(string userId)
    {
        var userExists = await _dbContext.OrderUsers.AnyAsync(x => x.Id == userId);
        if (userExists)
        {
            var orders = _dbContext.OrderOrders.Where(x => x.UserId == userId).ToList();
            return new ListOrdersResponse { Orders = orders };
        }

        throw new BusinessException($"User with id: {userId} not found");
    }
}