using Infrastructure.Services.Interfaces;
using MVC.Dtos;
using MVC.Models.Enums;
using MVC.Repositories.Interfaces;
using MVC.Services.Interfaces;
using MVC.ViewModels.CatalogViewModels;

namespace MVC.Services;

public class CatalogService : ICatalogService
{
    private readonly ICatalogRepository _catalogRepository;
    private readonly ILogger<CatalogService> _logger;

    public CatalogService(ILogger<CatalogService> logger, ICatalogRepository catalog)
    {
        _catalogRepository = catalog;
        _logger = logger;
    }

    public async Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? model, int? type, int? subType)
    {
        var filters = new Dictionary<CatalogFilter, int>();

        if (brand.HasValue) filters.Add(CatalogFilter.Brand, brand.Value);

        if (model.HasValue) filters.Add(CatalogFilter.Model, model.Value);

        if (type.HasValue) filters.Add(CatalogFilter.Type, type.Value);

        if (subType.HasValue) filters.Add(CatalogFilter.SubType, subType.Value);

        return await _catalogRepository.GetCatalogItems(new PaginatedItemsRequest<CatalogFilter>
        {
            PageIndex = page,
            PageSize = take,
            Filters = filters
        });
    }

    public async Task<CatalogItem> GetItemById(int id)
    {
        _logger.LogInformation($"item id in service {id}");
        var result = await _catalogRepository.GetItemById(id);
        _logger.LogInformation($"Item id from catalog {result.Id}, name {result.Name}");
        return result;
    }

    public async Task<IEnumerable<CatalogBrand>> GetBrands()
    {
        return await _catalogRepository.GetBrands();
    }

    public async Task<IEnumerable<CatalogModel>> GetModelsByBrand(int? selectedBrand)
    {
        return await _catalogRepository.GetModelsByBrand(selectedBrand);
    }

    public async Task<IEnumerable<CatalogType>> GetTypes()
    {
        return await  _catalogRepository.GetTypes();
    }

    public async Task<IEnumerable<CatalogSubType>> GetSubTypesByType(int? selectedType)
    {
        return await _catalogRepository.GetSubTypesByType(selectedType);
    }
}