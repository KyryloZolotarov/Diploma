using System.Net;
using Catalog.Host.Models.Requests.AddRequsts;
using Catalog.Host.Models.Requests.UpdateRequsts;
using Catalog.Host.Models.Responses.AddResponses;
using Catalog.Host.Models.Responses.UpdateResponses;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogBrandController : ControllerBase
    {
        private readonly ILogger<CatalogBrandController> _logger;
        private readonly ICatalogBrandService _catalogBrandService;

        public CatalogBrandController(
            ILogger<CatalogBrandController> logger,
            ICatalogBrandService catalogBrandService)
        {
            _logger = logger;
            _catalogBrandService = catalogBrandService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddBrandResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(AddBrandRequest request)
        {
            var result = await _catalogBrandService.Add(request.Id, request.BrandName);
            return Ok(new AddBrandResponse<int?>() { Id = result });
        }

        [HttpPut]
        [ProducesResponseType(typeof(UpdateBrandResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(UpdateBrandRequest request)
        {
            var result = await _catalogBrandService.Update(request.Id, request.BrandName);
            return Ok(new UpdateBrandResponse<int?>() { Id = result });
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            var result = await _catalogBrandService.Delete(id);
            return NoContent();
        }
    }
}
