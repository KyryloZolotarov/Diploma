using Infrastructure.Services.Interfaces;
using MVC.Models.Requests;
using MVC.Models.Responses;
using MVC.Repositories.Interfaces;
using MVC.ViewModels.OrderViewModels;

namespace MVC.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private readonly IHttpClientService _httpClient;
        private readonly ILogger<OrderRepository> _logger;
        private readonly IOptions<AppSettings> _settings;

        public OrderRepository(IHttpClientService httpClient, ILogger<OrderRepository> logger, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
        }
        public async Task<bool> AddOrder(ListOrderItemsRequest order)
        {
            return await _httpClient.SendAsync<bool, ListOrderItemsRequest>(
                $"{_settings.Value.OrderUrl}/AddOrder", HttpMethod.Post, order);
        }
        public async Task<ListOrderItemsResponse> GetOrder(int id)
        {
            return await _httpClient.SendAsync<ListOrderItemsResponse, int>(
                $"{_settings.Value.OrderUrl}/GetOrder", HttpMethod.Post, id);
        }
        public async Task<ListOrderResponse> GetOrderList()
        {
            return await _httpClient.SendAsync<ListOrderResponse>(
                $"{_settings.Value.OrderUrl}/GetOrderList", HttpMethod.Get);
        }
    }
}
