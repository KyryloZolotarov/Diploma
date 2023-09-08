using MVC.Models.Requests;
using MVC.Models.Responses;
using MVC.Services.Interfaces;
using MVC.ViewModels.BasketViewModels;
using MVC.ViewModels.OrderViewModels;

namespace MVC.Services
{
    public class OrderService : IOrderService
    {
        private readonly IOptions<AppSettings> _settings;
        private readonly IHttpClientService _httpClient;
        private readonly ILogger<OrderService> _logger;

        public OrderService(IHttpClientService httpClient, ILogger<OrderService> logger, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
        }

        public async Task<bool> AddOrder(ListOrderItemsFordDisplay order)
        {
            var orderForDb = new ListOrderItemsRequest() { Items = new List<OrderItemRequest>(), DateTime = order.DateTime};
            var result1 =
                await _httpClient.SendAsync<bool, ListOrderItemsRequest>(
                    $"{_settings.Value.CatalogUrl}/ChangeAvailableItems", HttpMethod.Post, orderForDb);
            
            foreach (var item in order.Items)
            {
                orderForDb.Items.Add(new OrderItemRequest()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Count = item.Count,
                    CatalogModelId = item.CatalogModelId,
                    CatalogSubTypeId = item.CatalogSubTypeId,
                    OrderId = item.OrderId,
                    Price = item.Price,
                    Order = new OrderToDb()
                    {
                        DateTime = order.DateTime
                    }
                });
            }
            var result =
                await _httpClient.SendAsync<bool, ListOrderItemsRequest>(
                    $"{_settings.Value.OrderUrl}/AddOrder", HttpMethod.Post, orderForDb);
            return result;

        }

        public async Task<ListOrderItemsFordDisplay> GetOrder(int id)
        {
            var result =
                await _httpClient.SendAsync<ListOrderItemsResponse, int>(
                    $"{_settings.Value.OrderUrl}/GetOrder", HttpMethod.Post, id);

            var orderFordDisplay = new ListOrderItemsFordDisplay() { Items = new List<OrderItemFordDisplay>(), DateTime = result.Order.DateTime};
            foreach (var item in result.Items)
            {
                orderFordDisplay.Items.Add(new OrderItemFordDisplay()
                {
                    Id = item.Id,
                    Name = item.Name,
                    Count = item.Count,
                    CatalogModelId = item.CatalogModelId,
                    CatalogSubTypeId = item.CatalogSubTypeId,
                    OrderId = item.OrderId, Price = item.Price,
                    Order = new OrderForDisplay()
                    {
                        Id = item.Order.Id,
                        UserId = item.Order.UserId,
                        DateTime = item.Order.DateTime
                    }
                });
            }
            return orderFordDisplay;
        }

        public async Task<ListOrdersForDisplay> GetOrderList()
        {
            var result =
                await _httpClient.SendAsync<ListOrderResponse>(
                    $"{_settings.Value.OrderUrl}/GetOrderList", HttpMethod.Get);
            var ordersForDisplay = new ListOrdersForDisplay() { Orders = new List<OrderForDisplay>() };
            if(result == null)
            {
                return ordersForDisplay;
            }    
            foreach (var item in result.Orders)
            {
                ordersForDisplay.Orders.Add(new OrderForDisplay()
                    { Id = item.Id, UserId = item.UserId, DateTime = item.DateTime });
            }
            return ordersForDisplay;

        }
    }
}
