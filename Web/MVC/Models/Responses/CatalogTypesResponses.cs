using MVC.ViewModels;

namespace MVC.Models.Responses
{
    public class CatalogTypesResponses
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public List<CatalogType> Types { get; set; }
    }
}
