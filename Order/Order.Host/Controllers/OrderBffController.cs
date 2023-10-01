using System.Security.Claims;
using IdentityModel;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;
using Order.Hosts.Models.ToFrontResponses;
using Order.Hosts.Services.Interfaces;

namespace Order.Hosts.Controllers;

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
        var user = User.GetClaims();
        var result = await _orderService.AddOrder(user, order);
        return Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(OrderOrderForFrontResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetOrder([FromBody] int id)
    {
        var user = User.GetClaims();
        var result = await _orderService.GetOrder(user.Id, id);
        return Ok(result);
    }

    [HttpGet]
    [ProducesResponseType(typeof(ListOrderForFrontResponse), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> GetOrderList()
    {
        var user = User.GetClaims();
        var result = await _orderService.GetOrderList(user.Id);
        return Ok(result);
    }
}