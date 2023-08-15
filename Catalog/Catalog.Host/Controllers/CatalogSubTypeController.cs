using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
    public class CatalogSubTypeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
