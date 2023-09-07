using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;
using Order.Hosts.Models.Responses;
using Order.Hosts.Services.Interfaces;

namespace Order.Hosts.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderBffController : ControllerBase
    {
        private readonly ILogger<OrderBffController> _logger;
        private readonly IOrderService _orderService;

        public OrderBffController(
            ILogger<OrderBffController> logger,
            IOrderService orderService)
        {
            _logger = logger;
            _orderService = orderService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(bool), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddOrder(AddOrderFromMVCRequest order)
        {
            var user = new OrderUserDto();
            user.Id = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            user.Name = User.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
            user.GivenName = User.Claims.FirstOrDefault(x => x.Type == "givenname")?.Value;
            user.FamilyName = User.Claims.FirstOrDefault(x => x.Type == "familyname")?.Value;
            user.Email = User.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
            user.Address = User.Claims.FirstOrDefault(x => x.Type == "adress")?.Value;
            var orderAdding = new OrderItemDto()
            {
                Id = order.Id,
                Name = order.Name,
                CatalogModelId = order.CatalogModelId,
                CatalogSubTypeId = order.CatalogSubTypeId,
                Count = order.Count,
            };
            var result = await _orderService.AddOrder(user, orderAdding);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderOrderResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrder(int id)
        {
            var result = await _orderService.GetOrder(id);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(ListOrdersResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderList()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var result = await _orderService.GetOrderList(userId);
            return Ok(result);
        }
    }
}
