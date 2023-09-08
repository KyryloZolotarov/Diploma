using System.Security.Claims;
using IdentityModel;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Order.Hosts.Models.BaseResponses;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;
using Order.Hosts.Models.ToFrontResponses;
using Order.Hosts.Services.Interfaces;

namespace Order.Hosts.Controllers
{
    [ApiController]

    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderBffController : ControllerBase
    {
        private readonly List<Claim> _userClaims = new ()
                {
                    new Claim(JwtClaimTypes.Subject, "1"),
                    new Claim(JwtClaimTypes.Name, "pirozok"),
                    new Claim(JwtClaimTypes.GivenName, "Alice"),
                    new Claim(JwtClaimTypes.FamilyName, "Smith"),
                    new Claim(JwtClaimTypes.Email, "AliceSmith@email.com"),
                    new Claim(JwtClaimTypes.EmailVerified, "true", ClaimValueTypes.Boolean),
                    new Claim(JwtClaimTypes.WebSite, "http://alice.com"),
                    new Claim(JwtClaimTypes.Address, @"{ 'street_address': 'One Hacker Way', 'locality': 'Heidelberg', 'postal_code': 69118, 'country': 'Germany' }")
                };
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
            var claims = User.Claims;
            foreach (var claim in claims)
            {
                switch (claim.Type)
                {
                    case JwtClaimTypes.Subject:
                        user.Id = claim.Value;
                        break;
                    case JwtClaimTypes.GivenName:
                        user.GivenName = claim.Value;
                        break;
                    case JwtClaimTypes.Name:
                        user.Name = claim.Value;
                        break;
                    case JwtClaimTypes.FamilyName:
                        user.FamilyName = claim.Value;
                        break;
                    case JwtClaimTypes.Email:
                        user.Email = claim.Value;
                        break;
                    case JwtClaimTypes.Address:
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
            var userId = _userClaims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var result = await _orderService.GetOrderList(userId);
            return Ok(result);
        }
    }
}
