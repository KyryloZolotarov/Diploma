using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
    public class CatalogBrandController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
