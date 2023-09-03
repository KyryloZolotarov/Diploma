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
    [Scope("basket.basketbff")]
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
        public async Task<IActionResult> AddItem(int itenId)
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            await _basketService.Add(basketId!, itenId);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddItems(BasketItem item)
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            await _basketService.AddItems(basketId!, item);
            return Ok();
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BasketItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangeItemsCount(BasketItem item)
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var response = await _basketService.ChangeItemsCount(basketId!, item);
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BasketItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var response = await _basketService.Get(basketId!);
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete()
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            await _basketService.Delete(basketId!);
            return NotFound();
        }

        [HttpDelete]
        [ProducesResponseType(typeof(IEnumerable<BasketItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteItem(int itemId)
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            var response = await _basketService.DeleteItem(basketId!, itemId);
            return Ok(response);
        }
    }
}
