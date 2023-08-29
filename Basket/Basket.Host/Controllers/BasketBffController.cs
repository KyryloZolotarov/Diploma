using Infrastructure.Identity;
using Infrastructure;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Basket.Host.Services.Interfaces;
using Basket.Host.Models;

namespace Basket.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Scope("catalog.basketbff")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class BasketBffController : ControllerBase
    {
        private readonly ILogger<BasketBffController> _logger;
        private readonly IBasketService _basketService;

        public BasketBffController(
            ILogger<BasketBffController> logger,
            IBasketService basketService)
        {
            _logger = logger;
            _basketService = basketService;
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> TestAdd(AddRequest data)
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            await _basketService.TestAdd(basketId!, data.Data);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(GetResponse), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> TestGet()
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var response = await _basketService.TestGet(basketId!);
            return Ok(response);
        }
    }
}
