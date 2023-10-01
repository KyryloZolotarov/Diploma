using IdentityModel;
using MVC.Services.Interfaces;
using MVC.ViewModels.BasketViewModels;
using MVC.ViewModels.OrderViewModels;

namespace MVC.Controllers;

public class OrderController : Controller
{
    private readonly ILogger<OrderController> _logger;
    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService, ILogger<OrderController> logger)
    {
        _orderService = orderService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        var response = await _orderService.GetOrderList();
        var user = User.GetClaims();
        response.User = user;
        return View(response);
    }

    [HttpPost]
    public async Task<IActionResult> CreateOrder(BasketIndexViewModel items)
    {
        var order = new ListOrderItemsFordDisplay { Items = new List<OrderItemFordDisplay>(), DateTime = DateTime.Now };
        foreach (var item in items.BasketItems)
            order.Items.Add(new OrderItemFordDisplay
            {
                Id = item.Id,
                Name = item.Name,
                CatalogModelId = item.CatalogModel.Id,
                CatalogSubTypeId = item.CatalogSubType.Id,
                Count = item.Count,
                Price = item.Price
            });
        var response = await _orderService.AddOrder(order);
        return View(items);
    }

    public async Task<IActionResult> SingleOrder(int id)
    {
        var response = await _orderService.GetOrder(id);

        return View(response);
    }
}