using Infrastructure.Exceptions;
using Order.Host.Data;
using Order.Hosts.Data.Entities;
using Order.Hosts.Models.BaseResponses;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;
using Order.Hosts.Repositories.Interfaces;

namespace Order.Hosts.Repositories
{
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
            var order = await _dbContext.OrderOrders.AddAsync(new OrderOrderEntity()
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
            if (orderExists == true)
            {
                var order = _dbContext.OrderOrders.Update(new OrderOrderEntity()
                {
                    UserId = userId,
                    DateTime = dateTime
                });
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Brand {order.Entity.Id}");
                return order.Entity.Id;
            }
            else
            {
                throw new BusinessException($"Brand id: {userId} was not founded");
            }
        }

        public async Task<int?> Delete(int id)
        {
            var orderExists = await _dbContext.OrderOrders.AnyAsync(x => x.Id == id);
            if (orderExists == true)
            {
                var orderDelete = await _dbContext.OrderOrders.FirstAsync(h => h.Id == id);
                _dbContext.OrderOrders.Remove(orderDelete);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Order Id Deleted {orderDelete.Id}");
                return orderDelete.Id;
            }
            else
            {
                throw new BusinessException($"Order Id {id} was not founded");
            }
        }

        public async Task<bool> AddOrder(OrderUserDto user, ListItemsForFrontRequest order)
        {
            var userExists = await _dbContext.OrderUsers.AnyAsync(x => x.Id == user.Id);
            var user1 = new OrderUserEntity()
            {
                Id = user.Id,
                Name = user.Name,
                GivenName = user.GivenName,
                FamilyName = user.FamilyName,
                Email = user.Email,
                Address = user.Address
            };
            if (userExists != true)
            {
                await _dbContext.OrderUsers.AddAsync(user1);
            }

            var orderAdding = await _dbContext.OrderOrders.AddAsync(new OrderOrderEntity()
            {
                UserId = user.Id,
                DateTime = order.DateTime,
                User = new OrderUserEntity()
                {
                    Id = user1.Id,
                    Address = user1.Address,
                    Email = user1.Email,
                    FamilyName = user1.FamilyName,
                    GivenName = user1.GivenName,
                    Name = user1.Name
                }
            });

            var items = new List<OrderItemEntity>();

            foreach (var item in order.Items)
            {
                items.Add(new OrderItemEntity()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Price = item.Price,
                    CatalogModelId = item.CatalogModelId,
                    CatalogSubTypeId = item.CatalogSubTypeId,
                    OrderId = orderAdding.Entity.Id,
                    Order = new OrderOrderEntity()
                    {
                        DateTime = orderAdding.Entity.DateTime,
                        UserId = orderAdding.Entity.UserId,
                        User = new OrderUserEntity()
                        {
                            Id = user1.Id,
                            Address = user1.Address,
                            Email = user1.Email,
                            FamilyName = user1.FamilyName,
                            GivenName = user1.GivenName,
                            Name = user1.Name
                        }
                    }
                });
            }

            await _dbContext.OrderItems.AddRangeAsync(items);
            await _dbContext.SaveChangesAsync();

            _logger.LogInformation($"Order id {orderAdding.Entity.Id} added");
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
                    Items = new ()
                };
                itemsList.Items.AddRange(items);
                itemsList.Order.Id = order.Id;
                itemsList.Order.DateTime = order.DateTime;
                itemsList.Order.UserId = order.UserId;
                itemsList.Order.User = order.User;
                return itemsList;
            }
            else
            {
                throw new BusinessException($"Order with id: {id} not found");
            }
        }

        public async Task<ListOrdersResponse> GetOrderList(string userId)
        {
            var userExists = await _dbContext.OrderUsers.AnyAsync(x => x.Id == userId);
            if (userExists)
            {
                var orders = _dbContext.OrderOrders.Where(x => x.UserId == userId).ToList();
                return new ListOrdersResponse() { Orders = orders };
            }
            else
            {
                throw new BusinessException($"User with id: {userId} not found");
            }
        }
    }
}
