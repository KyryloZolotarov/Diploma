using Catalog.Host.Models.Requests.AddRequsts;
using Catalog.Host.Models.Requests.UpdateRequsts;
using Catalog.Host.Models.Responses;
using Catalog.Host.Models.Responses.AddResponses;
using Catalog.Host.Models.Responses.UpdateResponses;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogTypeController : Controller
    {
        private readonly ILogger<CatalogTypeController> _logger;
        private readonly ICatalogTypeService _catalogTypeService;

        public CatalogTypeController(
            ILogger<CatalogTypeController> logger,
            ICatalogTypeService catalogTypeService)
        {
            _logger = logger;
            _catalogTypeService = catalogTypeService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddTypeResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(AddTypeRequest request)
        {
            var result = await _catalogTypeService.Add(request.Id, request.TypeName);
            return Ok(new AddTypeResponse<int?>() { Id = result });
        }

        [HttpPatch]
        [ProducesResponseType(typeof(AddTypeResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(UpdateTypeRequest request)
        {
            var result = await _catalogTypeService.Update(request.Id, request.TypeName);
            return Ok(new UpdateTypeResponse<int?>() { Id = result });
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _catalogTypeService.Delete(id);
            return NoContent();
        }
    }
}
