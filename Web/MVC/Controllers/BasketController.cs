using Infrastructure.Identity;
using MVC.Services.Interfaces;
using MVC.ViewModels.BasketViewModels;

namespace MVC.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;

        public BasketController(IBasketService basketService)
        {
            _basketService = basketService;
        }

        public async Task<IActionResult> Index()
        {
            var basket = await _basketService.GetBasket();
            var vm = new IndexViewModel();
            vm.BasketItems = basket;
            return View(vm);
        }

        public async Task<IActionResult> AddItemToBasket(int? itemId)
        {
            await _basketService.AddItemToBasket(itemId);
            return NoContent();
        }

        public async Task<IActionResult> ChangeItemsCountInBasket(int itemId, int itemsCount)
        {
            var vm = await _basketService.ChangeItemsCountInBasket(itemId, itemsCount);
            return View(vm);
        }

        public async Task<IActionResult> AddItemsInBasket(int itemId, int itemsCount)
        {
            await _basketService.AddItemsInBasket(itemId, itemsCount);
            return NoContent();
        }

        public async Task<IActionResult> DeleteItemFromBasket(int? itemId)
        {
            var vm = await _basketService.DeleteItemFromBasket(itemId);
            return View(vm);
        }

        public async Task<IActionResult> ClearBasket()
        {
            await _basketService.ClearBasket();
            return NoContent();
        }
    }
}
