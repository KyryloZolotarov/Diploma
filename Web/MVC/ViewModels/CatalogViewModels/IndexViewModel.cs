using MVC.ViewModels.Pagination;

namespace MVC.ViewModels.CatalogViewModels;

public class IndexViewModel
{
    public IEnumerable<CatalogItem> CatalogItems { get; set; }
    public IEnumerable<SelectListItem> Brands { get; set; }
    public IEnumerable<SelectListItem> Types { get; set; }
    public IEnumerable<SelectListItem> Models { get; set; }
    public IEnumerable<SelectListItem> SubTypes { get; set; }
    public int? BrandFilterApplied { get; set; }
    public int? ModelFilterApplied { get; set; }
    public int? SubTypeFilterApplied { get; set; }
    public int? TypesFilterApplied { get; set; }
    public PaginationInfo PaginationInfo { get; set; }
}
