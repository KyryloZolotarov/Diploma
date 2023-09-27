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
[Scope("catalog.catalogsubtype")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogSubTypeController : Controller
{
    private readonly ICatalogSubTypeService _catalogSubTypeService;
    private readonly ILogger<CatalogSubTypeController> _logger;

    public CatalogSubTypeController(
        ILogger<CatalogSubTypeController> logger,
        ICatalogSubTypeService catalogSubTypeService)
    {
        _logger = logger;
        _catalogSubTypeService = catalogSubTypeService;
    }

    [HttpPost]
    [ProducesResponseType(typeof(AddSubTypeResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Add(AddSubTypeRequest request)
    {
        try
        {
            var result = await _catalogSubTypeService.Add(request.SubTypeName, request.CatalogTypeId);
            return Ok(new AddSubTypeResponse<int?> { Id = result });
        }
        catch (BusinessException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateSubTypeResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateSubTypeRequest request)
    {
        try
        {
            var result = await _catalogSubTypeService.Update(request.Id, request.SubTypeName, request.CatalogTypeId);
            return Ok(new UpdateSubTypeResponse<int?> { Id = result });
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
            var result = await _catalogSubTypeService.Delete(id);
            return NoContent();
        }
        catch (BusinessException ex)
        {
            return NotFound(ex);
        }
    }
}