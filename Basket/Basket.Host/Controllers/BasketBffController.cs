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
        public async Task<IActionResult> AddItem([FromBody]int itemId)
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value ?? string.Empty;
            _logger.LogInformation($"Item id: {itemId}, basket id: {basketId}");
            await _basketService.Add(basketId, itemId);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> AddItems(BasketItem item)
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            _logger.LogInformation($"Item id: {item.Id}, items count {item.Count} basket id: {basketId}");
            await _basketService.AddItems(basketId!, item);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<BasketItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> ChangeItemsCount(BasketItem item)
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            _logger.LogInformation($"Item id: {item.Id}, items count {item.Count} basket id: {basketId}");
            var response = await _basketService.ChangeItemsCount(basketId!, item);
            foreach (var basketitem in response.Items!)
            {
                _logger.LogInformation($"item in basket id: {basketitem.Id}, items count: {basketitem.Count}");
            }
            return Ok(response);
        }

        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<BasketItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Get()
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            _logger.LogInformation($"basket id: {basketId}");
            var response = await _basketService.Get(basketId!);
            if (response ==null || response.Items==null)
            {
                return BadRequest();
            }
            foreach (var basketitem in response.Items)
            {
                _logger.LogInformation($"item in basket id: {basketitem.Id}, items count: {basketitem.Count}");
            }
            return Ok(response);
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        public async Task<IActionResult> Delete()
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            _logger.LogInformation($"basket id: {basketId}");
            await _basketService.Delete(basketId!);
            return Ok();
        }

        [HttpPost]
        [ProducesResponseType(typeof(IEnumerable<BasketItem>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> DeleteItem([FromBody]int itemId)
        {
            var basketId = User.Claims.FirstOrDefault(x => x.Type == "sub")?.Value;
            _logger.LogInformation($"Item id: {itemId}, basket id: {basketId}");
            var response = await _basketService.DeleteItem(basketId!, itemId);
            foreach (var basketitem in response.Items!)
            {
                _logger.LogInformation($"item in basket id: {basketitem.Id}, items count: {basketitem.Count}");
            }
            return Ok(response);
        }
    }
}
