using Catalog.Host.Models.Requests.AddRequsts;
using Catalog.Host.Models.Requests.UpdateRequsts;
using Catalog.Host.Models.Responses.AddResponses;
using Catalog.Host.Models.Responses.UpdateResponses;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
    [ApiController]
    [Authorize(Policy = AuthPolicy.AllowClientPolicy)]
    [Scope ("catalog.catalogitem")]
    [Route(ComponentDefaults.DefaultRoute)]
    public class CatalogItemController : Controller
    {
        private readonly ILogger<CatalogItemController> _logger;
        private readonly ICatalogItemService _catalogItemService;

        public CatalogItemController(
            ILogger<CatalogItemController> logger,
            ICatalogItemService catalogItemService)
        {
            _logger = logger;
            _catalogItemService = catalogItemService;
        }

        [HttpPost]
        [ProducesResponseType(typeof(AddItemResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Add(AddItemRequest request)
        {
            var result = await _catalogItemService.Add(request.Name, request.Description, request.Price, request.AvailableStock, request.PictureFileName, request.CatalogSubTypeId, request.CatalogModelId, request.PartNumber);
            return Ok(new AddItemResponse<int?>() { Id = result });
        }

        [HttpPut]
        [ProducesResponseType(typeof(UpdateItemResponse<int?>), (int)HttpStatusCode.OK)]
        public async Task<IActionResult> Update(UpdateItemRequest request)
        {
            var result = await _catalogItemService.Update(request.Id, request.Name, request.Description, request.Price, request.AvailableStock, request.PictureFileName, request.CatalogSubTypeId, request.CatalogModelId, request.PartNumber);
            return Ok(new UpdateItemResponse<int?>() { Id = result });
        }

        [HttpDelete]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Delete(int id)
        {
            await _catalogItemService.Delete(id);
            return NoContent();
        }
    }
}
