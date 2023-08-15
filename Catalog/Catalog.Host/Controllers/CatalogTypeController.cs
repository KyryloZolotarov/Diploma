using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
    public class CatalogTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
