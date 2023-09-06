using Microsoft.AspNetCore.Mvc;
using MVC.Services.Interfaces;

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
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult CreateOrder()
        {
            return NoContent();
        }
    }
}
