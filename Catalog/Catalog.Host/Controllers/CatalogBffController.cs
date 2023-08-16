using Catalog.Host.Models.Dtos;
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
            public async Task<IActionResult> Items(PaginatedItemsRequest request)
            {
                var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex);
                return Ok(result);
            }

            [HttpGet("{id}")]
            [ProducesResponseType(typeof(CatalogItemDto), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetItemById([FromRoute] int id)
            {
                var result = await _catalogService.GetCatalogItemByIdAsync(id);
                return Ok(result);
            }

            [HttpPost]
            [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetItemsByBrand(int brendId, PaginatedItemsRequest request)
            {
                var result = await _catalogService.GetCatalogItemsByBrandAsync(brendId, request.PageSize, request.PageIndex);
                return Ok(result);
            }

            [HttpPost]
            [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetItemsByType(int typeId, PaginatedItemsRequest request)
            {
                var result = await _catalogService.GetCatalogItemsByTypeAsync(typeId, request.PageSize, request.PageIndex);
                return Ok(result);
            }

            [HttpPost]
            [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetItemsBySubType(int subTypeId, PaginatedItemsRequest request)
            {
                var result = await _catalogService.GetCatalogItemsBySubTypeAsync(subTypeId, request.PageSize, request.PageIndex);
                return Ok(result);
            }

            [HttpPost]
            [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetItemsByModel(int modelId, PaginatedItemsRequest request)
            {
                var result = await _catalogService.GetCatalogItemsByModelAsync(modelId, request.PageSize, request.PageIndex);
                return Ok(result);
            }

            [HttpPost]
            [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogTypeDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetTypes(PaginatedItemsRequest request)
            {
                var result = await _catalogService.GetCatalogTypesAsync(request.PageSize, request.PageIndex);
                return Ok(result);
            }

            [HttpPost]
            [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogBrandDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetBrends(PaginatedItemsRequest request)
            {
                var result = await _catalogService.GetCatalogBrandsAsync(request.PageSize, request.PageIndex);
                return Ok(result);
            }
        }
}
