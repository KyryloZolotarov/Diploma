using Infrastructure.Identity;
using MVC.Services.Interfaces;
using MVC.ViewModels.BasketViewModels;

namespace MVC.Controllers
{
    public class BasketController : Controller
    {
        private readonly IBasketService _basketService;
        private readonly ILogger<BasketController> _logger;

        public BasketController(IBasketService basketService, ILogger<BasketController> logger)
        {
            _basketService = basketService;
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            var basket = await _basketService.GetBasket();
            var vm = new IndexViewModel();
            vm.BasketItems = basket;
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> AddItemToBasket(int id)
        {
            await _basketService.AddItemToBasket(id);
            _logger.LogInformation($"{id}");
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
