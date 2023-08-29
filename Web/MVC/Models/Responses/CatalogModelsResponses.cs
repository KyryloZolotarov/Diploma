using MVC.ViewModels;

namespace MVC.Models.Responses
{
    public class CatalogModelsResponses
    {
        public int PageIndex { get; set; }

        public int PageSize { get; set; }
        public List<CatalogModel> Models { get; set; }
    }
}
