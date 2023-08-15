using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
    public class CatalogItemController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
