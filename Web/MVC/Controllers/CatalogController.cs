using MVC.Services.Interfaces;
using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.Pagination;

namespace MVC.Controllers;

public class CatalogController : Controller
{
    private  readonly ICatalogService _catalogService;

    public CatalogController(ICatalogService catalogService)
    {
        _catalogService = catalogService;
    }

    public async Task<IActionResult> Index(int? brandFilterApplied, int? modelFilterApplied, int? typesFilterApplied, int? subTypeFilterApplied, int? page, int? itemsPage)
    {   
        page ??= 0;
        itemsPage ??= 6;
        
        var catalog = await _catalogService.GetCatalogItems(page.Value, itemsPage.Value, brandFilterApplied, modelFilterApplied, typesFilterApplied, subTypeFilterApplied);
        if (catalog == null)
        {
            return View("Error");
        }
        var info = new PaginationInfo()
        {
            ActualPage = page.Value,
            ItemsPerPage = catalog.Data.Count,
            TotalItems = catalog.Count,
            TotalPages = (int)Math.Ceiling((decimal)catalog.Count / itemsPage.Value)
        };
        var vm = new IndexViewModel()
        {
            CatalogItems = catalog.Data,
            Brands = (await _catalogService.GetBrands()).Select(x => new SelectListItem(x.Brand, x.Id.ToString())).Append(new SelectListItem("All", null)),
            Models = (await _catalogService.GetModels()).Select(x => new SelectListItem(x.Model, x.Id.ToString())).Append(new SelectListItem("All", null)),
            Types = (await _catalogService.GetTypes()).Select(x => new SelectListItem(x.Type, x.Id.ToString())).Append(new SelectListItem("All", null)),
            SubTypes = (await _catalogService.GetSubTypes()).Select(x => new SelectListItem(x.SubType, x.Id.ToString())).Append(new SelectListItem("All", null)),
            PaginationInfo = info
        };

        vm.PaginationInfo.Next = (vm.PaginationInfo.ActualPage == vm.PaginationInfo.TotalPages - 1) ? "is-disabled" : "";
        vm.PaginationInfo.Previous = (vm.PaginationInfo.ActualPage == 0) ? "is-disabled" : "";

        return View(vm);
    }
}