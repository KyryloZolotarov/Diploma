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

        public Task<IActionResult> Index()
        {
            var vm = _basketService.GetBasket();
            return View(vm);
        }

        public Task AddItemToBasket(int? itemId)
        {
            _basketService.AddItemToBasket(itemId);
            return View();
        }

        public Task DeleteItemFromBasket(int? itemId)
        {
            _basketService.DeleteItemFromBasket(itemId);
            return View();
        }

        public Task ClearBasket()
        {
            _basketService.ClearBasket();
            return View();
        }
    }
}
