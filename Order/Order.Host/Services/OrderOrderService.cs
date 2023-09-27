using System.Net;
using System.Xml.Linq;
using Infrastructure.Exceptions;
using Order.Host.Data;
using Order.Hosts.Data.Entities;
using Order.Hosts.Repositories;
using Order.Hosts.Repositories.Interfaces;
using Order.Hosts.Services.Interfaces;

namespace Order.Hosts.Services;

public class OrderOrderService : BaseDataService<ApplicationDbContext>, IOrderOrderService
{
    private readonly IOrderUserRepository _orderUserRepository;
    private readonly IOrderOrderRepository _orderOrderRepository;

    public OrderOrderService(
        IOrderUserRepository orderUserRepository,
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        IOrderOrderRepository orderOrderRepository)
        : base(dbContextWrapper, logger)
    {
        _orderUserRepository = orderUserRepository;
        _orderOrderRepository = orderOrderRepository;
    }

    public async Task<int?> Add(string userId, DateTime dateTime)
    {
        var userExists = await ExecuteSafeAsync(() => _orderUserRepository.CheckUserExist(userId));
        if (userExists == null)
        {
            throw new BusinessException($"User with id: {userId} not found");
        }

        var order = new OrderOrderEntity()
        {
            UserId = userId,
            DateTime = dateTime
        };

        return await ExecuteSafeAsync(() => _orderOrderRepository.Add(order));
    }

    public async Task<int?> Update(int id, string userId, DateTime dateTime)
    {
        var orderExists = await ExecuteSafeAsync(() => _orderOrderRepository.CheckOrderExist(id));
        if (orderExists == null)
        {
            throw new BusinessException($"Order with id: {id} not found");
        }

        orderExists.UserId = userId;
        orderExists.DateTime = dateTime;
        orderExists.Id = id;

        return await ExecuteSafeAsync(() => _orderOrderRepository.Update(orderExists));
    }

    public async Task<int?> Delete(int id)
    {
        var order = await ExecuteSafeAsync(() => _orderOrderRepository.CheckOrderExist(id));
        if (order == null)
        {
            throw new BusinessException($"Order with id: {id} not found");
        }

        return await ExecuteSafeAsync(() => _orderOrderRepository.Delete(order));
    }
}