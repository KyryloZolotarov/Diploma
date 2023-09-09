using System.Security.Claims;
using IdentityModel;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;
using Order.Hosts.Models.ToFrontResponses;
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
        public async Task<IActionResult> AddOrder([FromBody] ListItemsForFrontRequest order)
        {
            var user = new OrderUserDto();
            var claims = User.Claims.ToList();
            foreach (var claim in claims)
            {
                switch (claim.Type)
                {
                    case JwtClaimTypes.Subject:
                    case ClaimTypes.NameIdentifier:
                        user.Id = claim.Value;
                        break;
                    case ClaimTypes.GivenName:
                    case JwtClaimTypes.GivenName:
                        user.GivenName = claim.Value;
                        break;
                    case ClaimTypes.Name:
                    case JwtClaimTypes.Name:
                        user.Name = claim.Value;
                        break;
                    case JwtClaimTypes.FamilyName:
                    case ClaimTypes.Surname:
                        user.FamilyName = claim.Value;
                        break;
                    case JwtClaimTypes.Email:
                    case ClaimTypes.Email:
                    case ClaimTypes.Upn:
                        user.Email = claim.Value;
                        break;
                    case JwtClaimTypes.Address:
                    case ClaimTypes.StreetAddress:
                        user.Address = claim.Value;
                        break;
                }
            }

            var result = await _orderService.AddOrder(user, order);
            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(typeof(OrderOrderForFrontResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrder([FromBody]int id)
        {
            var result = await _orderService.GetOrder(id);
            return Ok(result);
        }

        [HttpGet]
        [ProducesResponseType(typeof(ListOrderForFrontResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> GetOrderList()
        {
            var userId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var result = await _orderService.GetOrderList(userId);
            return Ok(result);
        }
    }
}
