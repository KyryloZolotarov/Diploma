using Catalog.Host.Data;
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
            [AllowAnonymous]
            [ProducesResponseType(typeof(PaginatedItemsResponse<CatalogItemDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> Items(PaginatedItemsRequest<CatalogFilter> request)
            {
                var result = await _catalogService.GetCatalogItemsAsync(request.PageSize, request.PageIndex, request.Filters);
                return Ok(result);
            }

            [HttpPost]
            [ProducesResponseType(typeof(BasketItems<CatalogItemDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> ListItems([FromBody] ItemsForBasketRequest request)
            {
                _logger.LogInformation($"items count {request.Items.Count}");
                var result = await _catalogService.GetListCatalogItemsAsync(request);
                foreach (var item in result.Items)
                {
                    _logger.LogInformation($"item id: {item.Id}");
                }

                return Ok(result);
            }

            [HttpPost]
            [AllowAnonymous]
            [ProducesResponseType(typeof(CatalogItemDto), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetItemById([FromBody] int id)
            {
                _logger.LogInformation($"item id {id}");
                var result = await _catalogService.GetCatalogItemByIdAsync(id);
                _logger.LogInformation($"item id {result.Id}, item name {result.Name}");
                return Ok(result);
            }

            [HttpGet]
            [AllowAnonymous]
            [ProducesResponseType(typeof(IEnumerable<CatalogTypeDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetTypes()
            {
                var result = await _catalogService.GetCatalogTypesAsync();
                return Ok(result);
            }

            [HttpGet]
            [AllowAnonymous]
            [ProducesResponseType(typeof(IEnumerable<CatalogBrandDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetBrands()
            {
                var result = await _catalogService.GetCatalogBrandsAsync();
                return Ok(result);
            }

            [HttpGet("{id}")]
            [AllowAnonymous]
            [ProducesResponseType(typeof(IEnumerable<CatalogSubTypeDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetSubTypes([FromRoute] int id)
            {
            var result = await _catalogService.GetCatalogSubTypesAsync(id);
            return Ok(result);
            }

            [HttpGet("{id}")]
            [AllowAnonymous]
            [ProducesResponseType(typeof(IEnumerable<CatalogModelDto>), (int)HttpStatusCode.OK)]
            public async Task<IActionResult> GetModels([FromRoute] int id)
            {
            var result = await _catalogService.GetCatalogModelsAsync(id);
            return Ok(result);
            }
        }
}
