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
[Scope("catalog.catalogbrand")]
[Route(ComponentDefaults.DefaultRoute)]
public class CatalogBrandController : ControllerBase
{
    private readonly ICatalogBrandService _catalogBrandService;
    private readonly ILogger<CatalogBrandController> _logger;

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
        try
        {
            var result = await _catalogBrandService.Add(request.BrandName);
            return Ok(new AddBrandResponse<int?> { Id = result });
        }
        catch (BusinessException ex)
        {
            return NotFound(ex.Message);
        }
    }

    [HttpPut]
    [ProducesResponseType(typeof(UpdateBrandResponse<int?>), (int)HttpStatusCode.OK)]
    public async Task<IActionResult> Update(UpdateBrandRequest request)
    {
        try
        {
            var result = await _catalogBrandService.Update(request.Id, request.BrandName);
            return Ok(new UpdateBrandResponse<int?> { Id = result });
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
            var result = await _catalogBrandService.Delete(id);
            return NoContent();
        }
        catch (BusinessException ex)
        {
            return NotFound(ex.Message);
        }
    }
}