using Catalog.Host.Models.Requests.AddRequsts;
using Catalog.Host.Models.Requests.UpdateRequsts;
using Catalog.Host.Models.Responses.AddResponses;
using Catalog.Host.Models.Responses.UpdateResponses;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Exceptions;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;

namespace Catalog.Host.Controllers;

[ApiController]
[Authorize(Policy = AuthPolicy.AllowClientPolicy)]
[Scope("catalog.catalogtype")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogTypeController : Controller
{
    private readonly ICatalogTypeService _catalogTypeService;
    private readonly ILogger<CatalogTypeController> _logger;

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
        try
        {
            var result = await _catalogTypeService.Add(request.TypeName);
            return Ok(new AddTypeResponse<int?> { Id = result });
        }
        catch (BusinessException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPatch]
    [ProducesResponseType(typeof(AddTypeResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateTypeRequest request)
    {
        try
        {
            var result = await _catalogTypeService.Update(request.Id, request.TypeName);
            return Ok(new UpdateTypeResponse<int?> { Id = result });
        }
        catch (BusinessException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpDelete]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Delete(int id)
    {
        try
        {
            var result = await _catalogTypeService.Delete(id);
            return NoContent();
        }
        catch (BusinessException ex)
        {
            return NotFound(ex);
        }
    }
}