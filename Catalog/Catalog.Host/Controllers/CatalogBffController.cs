using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Catalog.Host.Controllers
{
    public class CatalogBffController : Controller
    {
        [ApiController]
        [Route(ComponentDefaults.DefaultRoute)]
        public class CatalogBffController : ControllerBase
        {
            private readonly ILogger<CatalogBffController> _logger;
            private readonly ICatalogService _catalogService;

            public CatalogBffController(
                ILogger<CatalogBffController> logger,
                ICatalogService catalogService)
            {
                _logger = logger;
                _catalogService = catalogService;
            }

            [HttpPost]
            [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> Items(PaginatedItemsRequest<CatalogFilter> request)
            {
                var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex, request.Filters);
                return Ok(result);
            }

            [HttpGet("{id}")]
            [ProducesResponseType(typeof(CatalogItemDto), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetItemById([FromRoute] int id)
            {
                var result = await _catalogService.GetCatalogItemByIdAsync(id);
                return Ok(result);
            }

            [HttpGet]
            [ProducesResponseType(typeof(IEnumerable<CatalogTypeDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetTypes()
            {
                var result = await _catalogService.GetCatalogTypesAsync();
                return Ok(result);
            }

            [HttpGet]
            [ProducesResponseType(typeof(IEnumerable<CatalogBrandDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetBrands()
            {
                var result = await _catalogService.GetCatalogBrandsAsync();
                return Ok(result);
            }
        }
}
