using MVC.ViewModels.CatalogViewModels;

namespace MVC.Models.Responses
{
    public class CatalogBrandsResponses
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public List<CatalogBrand> Brands { get; set; }
    }
}
