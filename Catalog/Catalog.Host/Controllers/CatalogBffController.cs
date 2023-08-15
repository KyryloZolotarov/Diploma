using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
    public class CatalogBffController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
