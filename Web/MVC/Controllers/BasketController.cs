using MVC.Models.Requests;
using MVC.Services.Interfaces;
using MVC.ViewModels.BasketViewModels;

namespace MVC.Controllers;

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

    [HttpPost]
    public async Task<IActionResult> ChangeItemsCountInBasket([FromBody] BasketChangeRequest request)
    {
        var vm = await _basketService.ChangeItemsCountInBasket(request.Id, request.Count);
        return View("index", vm);
    }

    [HttpPost]
    public async Task<IActionResult> AddItemsInBasket([FromBody] BasketChangeRequest request)
    {
        await _basketService.AddItemsInBasket(request.Id, request.Count);
        return NoContent();
    }

    [HttpPost]
    public async Task<IActionResult> DeleteItemFromBasket(int id)
    {
        var vm = await _basketService.DeleteItemFromBasket(id);
        return View("index", vm);
    }

    [HttpPost]
    public async Task<IActionResult> ClearBasket()
    {
        await _basketService.ClearBasket();
        var vm = new BasketIndexViewModel { BasketItems = new List<BasketItemForDisplay>() };
        return View("index", vm);
    }
}