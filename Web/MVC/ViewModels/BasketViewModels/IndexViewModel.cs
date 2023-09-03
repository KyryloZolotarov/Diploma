using MVC.ViewModels.CatalogViewModels;
using MVC.ViewModels.Pagination;

namespace MVC.ViewModels.BasketViewModels
{
    public class IndexViewModel
    {
        public IEnumerable<BasketItemForDisplay> BasketItems { get; set; }
    }
}
