using MVC.ViewModels.Pagination;

namespace MVC.ViewModels.BasketViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<CatalogItem> CatalogItems { get; set; }
    }
}
