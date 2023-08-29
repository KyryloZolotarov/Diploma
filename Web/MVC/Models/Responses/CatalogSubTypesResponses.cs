using MVC.ViewModels;

namespace MVC.Models.Responses
{
    public class CatalogSubTypesResponses
    {

        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public List<CatalogSubType> SubTypes { get; set; }
    }
}
