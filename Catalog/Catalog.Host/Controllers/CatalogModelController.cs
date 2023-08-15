using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
    public class CatalogModelController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
