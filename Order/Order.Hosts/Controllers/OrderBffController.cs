using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;
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
        [ProducesResponseType(typeof(IEnumerable<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddOrder(AddOrderFromMVCRequest order)
        {
            var user = new OrderUserDto();
            user.Id = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            user.Name = User.Claims.FirstOrDefault(x => x.Type == "name")?.Value;
            user.GivenName = User.Claims.FirstOrDefault(x => x.Type == "givenname")?.Value;
            user.FamilyName = User.Claims.FirstOrDefault(x => x.Type == "familyname")?.Value;
            user.Email = User.Claims.FirstOrDefault(x => x.Type == "email")?.Value;
            user.Address = User.Claims.FirstOrDefault(x => x.Type == "adress")?.Value;
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrder(int id)
        {
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<int>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderList()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            return Ok();
        }
    }
}
