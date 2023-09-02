using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Responses;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.Host.Controllers
{
        [ApiController]
        [Authorize(Policy = AuthPolicy.AllowEndUserPolicy)]
        [Scope("catalog.catalogbff")]
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

            [AllowAnonymous]
            [HttpPost]
            [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> Items(PaginatedItemsRequest<CatalogFilter> request)
            {
                var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex, request.Filters);
                return Ok(result);
            }

            [AllowAnonymous]
            [HttpGet]
            [ProducesResponseType(typeof(ItemsForBasketResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> ListItems(ItemsForBasketRequest request)
            {
                var result = await _catalogService.GetListCatalogItemsAsync(request.Items);
                return Ok(result);
            }

            [HttpGet("{id}")]
            [ProducesResponseType(typeof(CatalogItemDto), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetItemById([FromRoute] int id)
            {
                var result = await _catalogService.GetCatalogItemByIdAsync(id);
                return Ok(result);
            }

            [AllowAnonymous]
            [HttpGet]
            [ProducesResponseType(typeof(IEnumerable<CatalogTypeDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetTypes()
            {
                var result = await _catalogService.GetCatalogTypesAsync();
                return Ok(result);
            }

            [AllowAnonymous]
            [HttpGet]
            [ProducesResponseType(typeof(IEnumerable<CatalogBrandDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetBrands()
            {
                var result = await _catalogService.GetCatalogBrandsAsync();
                return Ok(result);
            }

            [AllowAnonymous]
            [HttpGet("{id}")]
            [ProducesResponseType(typeof(IEnumerable<CatalogSubTypeDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetSubTypes([FromRoute] int id)
            {
            var result = await _catalogService.GetCatalogSubTypesAsync(id);
            return Ok(result);
            }

            [AllowAnonymous]
            [HttpGet("{id}")]
            [ProducesResponseType(typeof(IEnumerable<CatalogModelDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetModels([FromRoute] int id)
            {
            var result = await _catalogService.GetCatalogModelsAsync(id);
            return Ok(result);
            }
        }
}
