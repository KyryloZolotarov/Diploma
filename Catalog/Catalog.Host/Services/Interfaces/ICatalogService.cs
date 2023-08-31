using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Responses;

namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogService
    {
        Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogFilter, int>? filters);
        Task<IEnumerable<CatalogTypeDto>> GetCatalogTypesAsync();
        Task<IEnumerable<CatalogBrandDto>> GetCatalogBrandsAsync();
        Task<CatalogItemDto> GetCatalogItemByIdAsync(int id);
        Task<IEnumerable<CatalogModelDto>> GetCatalogModelsAsync(int id);
        Task<IEnumerable<CatalogSubTypeDto>> GetCatalogSubTypesAsync(int id);
    }
}
