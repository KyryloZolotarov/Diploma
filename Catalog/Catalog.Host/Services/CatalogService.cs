using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Responses;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
    {
        private readonly ICatalogItemRepository _catalogItemRepository;
        private readonly IMapper _mapper;

        public CatalogService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogItemRepository catalogItemRepository,
            IMapper mapper)
            : base(dbContextWrapper, logger)
        {
            _catalogItemRepository = catalogItemRepository;
            _mapper = mapper;
        }

        public async Task<PaginatedItemsResponse<CatalogItemDto>?> GetCatalogItemsAsync(int pageSize, int pageIndex, Dictionary<CatalogFilter, int>? filters)
        {
            return await ExecuteSafeAsync(async () =>
            {
                int? brandFilter = null;
                int? typeFilter = null;
                int? subTypeFilter = null;
                int? modelFilter = null;

                if (filters != null)
                {
                    if (filters.TryGetValue(CatalogFilter.Brand, out var brand))
                    {
                        brandFilter = brand;
                    }

                    if (filters.TryGetValue(CatalogFilter.Type, out var type))
                    {
                        typeFilter = type;
                    }

                    if (filters.TryGetValue(CatalogFilter.SubType, out var subtype))
                    {
                        subTypeFilter = subtype;
                    }

                    if (filters.TryGetValue(CatalogFilter.Model, out var model))
                    {
                        modelFilter = model;
                    }
                }

                var result = await _catalogItemRepository.GetByPageAsync(pageIndex, pageSize, brandFilter, typeFilter, subTypeFilter, modelFilter);
                if (result == null)
                {
                    return null;
                }

                return new PaginatedItemsResponse<CatalogItemDto>()
                {
                    Count = result.TotalCount,
                    Data = result.Data.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList(),
                    PageIndex = pageIndex,
                    PageSize = pageSize
                };
            });
        }

        public async Task<CatalogItemDto> GetCatalogItemByIdAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                var result = await _catalogItemRepository.GetByIdAsync(id);
                return _mapper.Map<CatalogItemDto>(result);
            });
        }

        public async Task<IEnumerable<CatalogTypeDto>> GetCatalogTypesAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                return (await _catalogItemRepository.GetTypesAsync()).Select(s => _mapper.Map<CatalogTypeDto>(s)).ToList();
            });
        }

        public async Task<IEnumerable<CatalogBrandDto>> GetCatalogBrandsAsync()
        {
            return await ExecuteSafeAsync(async () =>
            {
                return (await _catalogItemRepository.GetBrandsAsync()).Select(s => _mapper.Map<CatalogBrandDto>(s)).ToList();
            });
        }

        public async Task<IEnumerable<CatalogSubTypeDto>> GetCatalogSubTypesAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return (await _catalogItemRepository.GetSubTypesAsync(id)).Select(s => _mapper.Map<CatalogSubTypeDto>(s)).ToList();
            });
        }

        public async Task<IEnumerable<CatalogModelDto>> GetCatalogModelsAsync(int id)
        {
            return await ExecuteSafeAsync(async () =>
            {
                return (await _catalogItemRepository.GetModelsAsync(id)).Select(s => _mapper.Map<CatalogModelDto>(s)).ToList();
            });
        }
    }
}
