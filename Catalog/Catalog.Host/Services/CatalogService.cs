using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.UpdateRequsts;
using Catalog.Host.Models.Responses;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Exceptions;

namespace Catalog.Host.Services;

public class CatalogService : BaseDataService<ApplicationDbContext>, ICatalogService
{
    private readonly ICatalogItemRepository _catalogItemRepository;
    private readonly ICatalogModelRepository _catalogModelRepository;
    private readonly IMapper _mapper;

    public CatalogService(
        ICatalogModelRepository catalogModelRepository,
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository,
        IMapper mapper)
        : base(dbContextWrapper, logger)
    {
        _catalogModelRepository = catalogModelRepository;
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

            return new PaginatedItemsResponse<CatalogItemDto>
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

    public async Task<BasketItems<CatalogItemDto>> GetListCatalogItemsAsync(ItemsForBasketRequest items)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var ids = new List<int>();
            ids.AddRange(items.Items.Select(x => x.Id));
            var result = await _catalogItemRepository.GetItemsListAsync(ids);
            var mappedResult = new BasketItems<CatalogItemDto>
                { Items = result.Items.Select(s => _mapper.Map<CatalogItemDto>(s)).ToList() };
            return mappedResult;
        });
    }

    public async Task<bool> ChangeAvailableItems(UpdateAvailableItemsRequest item)
    {
        var updating = ExecuteSafeAsync(() => _catalogItemRepository.CheckItemExist(item.Id));
        if (item == null)
        {
            throw new BusinessException($"Item with id: {item.Id} not found");
        }

        updating.Result.AvailableStock = item.ChangeAvailable;

        var result = await ExecuteSafeAsync(() => _catalogItemRepository.Update(updating.Result));
        if (result == item.Id)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public async Task<CatalogModelsForOrderResponse> GetCatalogModelForOrder(CatalogModelsForOrderRequest modelIds)
    {
        return await ExecuteSafeAsync(async () =>
        {
            var result = await _catalogModelRepository.GetModelsForOrderAsync(modelIds);
            var mappedResult = new CatalogModelsForOrderResponse
                { Models = result.Select(s => _mapper.Map<CatalogModelDto>(s)).ToList() };
            return mappedResult;
        });
    }

    public async Task<IEnumerable<CatalogTypeDto>> GetCatalogTypesAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            return (await _catalogItemRepository.GetTypesAsync()).Select(s => _mapper.Map<CatalogTypeDto>(s))
                .ToList();
        });
    }

    public async Task<IEnumerable<CatalogBrandDto>> GetCatalogBrandsAsync()
    {
        return await ExecuteSafeAsync(async () =>
        {
            return (await _catalogItemRepository.GetBrandsAsync()).Select(s => _mapper.Map<CatalogBrandDto>(s))
                .ToList();
        });
    }

    public async Task<IEnumerable<CatalogSubTypeDto>> GetCatalogSubTypesAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            return (await _catalogItemRepository.GetSubTypesAsync(id))
                .Select(s => _mapper.Map<CatalogSubTypeDto>(s)).ToList();
        });
    }

    public async Task<IEnumerable<CatalogModelDto>> GetCatalogModelsAsync(int id)
    {
        return await ExecuteSafeAsync(async () =>
        {
            return (await _catalogItemRepository.GetModelsAsync(id)).Select(s => _mapper.Map<CatalogModelDto>(s))
                .ToList();
        });
    }
}