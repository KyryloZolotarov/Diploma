using Infrastructure.Identity;
using MVC.Services.Interfaces;

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

        public async Task<IActionResult> Index()
        {
            var vm =  await _basketService.GetBasket();
            return View(vm);
        }

        public async Task<IActionResult> AddItemToBasket(int? itemId)
        {
            await _basketService.AddItemToBasket(itemId);
            return View();
        }

        public async Task<IActionResult> DeleteItemFromBasket(int? itemId)
        {
            var vm = await _basketService.DeleteItemFromBasket(itemId);
            return View(vm);
        }

        public async Task<IActionResult> ClearBasket()
        {
            await _basketService.ClearBasket();
            return View();
        }
    }
}
