using Infrastructure.Identity;
using Microsoft.AspNetCore.Mvc;
using MVC.Services.Interfaces;
using MVC.ViewModels;

namespace MVC.Controllers
{
    [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
    [Scope("mvc.basket")]
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }
        public IActionResult GetBasket()
        {
            var vm = _basketService.GetBusket();
            return View(vm);
        }
        public IActionResult Index()
        {
            return View();
        }
    }
}
