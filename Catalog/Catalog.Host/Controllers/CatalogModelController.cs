using Catalog.Host.Models.Requests.AddRequsts;
using Catalog.Host.Models.Requests.UpdateRequsts;
using Catalog.Host.Models.Responses.AddResponses;
using Catalog.Host.Models.Responses.UpdateResponses;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogmodel")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogModelController : Controller
{
    private readonly ICatalogModelService _catalogModelService;
    private readonly ILogger<CatalogModelController> _logger;

    public CatalogModelController(
        ILogger<CatalogModelController> logger,
        ICatalogModelService catalogModelService)
    {
        _logger = logger;
        _catalogModelService = catalogModelService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddModelResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(AddModelRequest request)
    {
        var result = await _catalogModelService.Add(request.ModelName, request.CatalogBrandId);
        return Ok(new AddModelResponse<int?> { Id = result });
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateModelResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateModelRequest request)
    {
        var result = await _catalogModelService.Update(request.Id, request.ModelName, request.CatalogBrandId);
        return Ok(new UpdateModelResponse<int?> { Id = result });
    }

    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        var result = await _catalogModelService.Delete(id);
        return NoContent();
    }
}