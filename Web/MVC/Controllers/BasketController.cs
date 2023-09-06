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
            var vm = await _basketService.GetBasket();
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
            return View("index", vm);
        }

        public async Task<IActionResult> AddItemsInBasket(int id, int count)
        {
            await _basketService.AddItemsInBasket(id, count);
            return NoContent();
        }

        public async Task<IActionResult> DeleteItemFromBasket(int id)
        {
            var vm = await _basketService.DeleteItemFromBasket(id);
            return View("index", vm);
        }

        public async Task<IActionResult> ClearBasket()
        {
            await _basketService.ClearBasket();
            var vm = new BasketIndexViewModel() {BasketItems = new List<BasketItemForDisplay>()};
            return View("index", vm);
        }
    }
}
