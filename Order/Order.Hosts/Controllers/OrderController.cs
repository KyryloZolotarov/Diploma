using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Hosts.Data.Entities;
using Order.Hosts.Models.Requests;
using Order.Hosts.Models.Responses;
using Order.Hosts.Services.Interfaces;

namespace Order.Hosts.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Scope("order.order")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class OrderController : ControllerBase
    {
        private readonly ILogger<OrderController> _logger;
        private readonly IOrderOrderService _orderOrderService;

        public OrderController(
            ILogger<OrderController> logger,
            IOrderOrderService orderOrderService)
        {
            _logger = logger;
            _orderOrderService = orderOrderService;
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public OrderUserEntity User { get; set; }

        [HttpPost]
        [ProducesResponseType(typeof(BaseResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(AddOrderRequest request)
        {
            var result = await _orderOrderService.Add(request.UserId, request.DateTime);
            return Ok(new BaseResponse<int?>() { Id = result });
        }

        [HttpPut]
        [ProducesResponseType(typeof(BaseResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(UpdateOrderRequest request)
        {
            var result = await _orderOrderService.Update(request.UserId, request.DateTime);
            return Ok(new BaseResponse<int?>() { Id = result });
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _orderOrderService.Delete(id);
            return NoContent();
        }
    }
}
