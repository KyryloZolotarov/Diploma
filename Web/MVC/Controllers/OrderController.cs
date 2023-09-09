using IdentityModel;
using Microsoft.AspNetCore.Mvc;
using MVC.Services.Interfaces;
using MVC.ViewModels.BasketViewModels;
using MVC.ViewModels.OrderViewModels;

namespace MVC.Controllers
{
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly ILogger<OrderController> _logger;

        public OrderController(IOrderService orderService, ILogger<OrderController> logger)
        {
            _orderService = orderService;
            _logger = logger;
        }
        public async Task<IActionResult> Index()
        {
            var response = await _orderService.GetOrderList();
            var user = new OrderUserForDisplay();
            var claims = User.Claims.ToList();
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
            response.User = user;
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> CreateOrder(BasketIndexViewModel items)
        {
            var order = new ListOrderItemsFordDisplay() { Items = new List<OrderItemFordDisplay>() , DateTime = DateTime.Now};
            foreach (var item in items.BasketItems)
            {
                order.Items.Add(new OrderItemFordDisplay()
                {
                    Id = item.Id,
                    Name = item.Name,
                    CatalogModelId = item.CatalogModel.Id,
                    CatalogSubTypeId = item.CatalogSubType.Id,
                    Count = item.Count,
                    Price = item.Price
                });
            }
            var response = await _orderService.AddOrder(order);
            return View();
        }

        public async Task<IActionResult> SingleOrder(int id)
        {
            var response = await _orderService.GetOrder(id);
            return View(response);
        }
    }
}
