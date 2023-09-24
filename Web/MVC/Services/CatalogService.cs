using Infrastructure.Services.Interfaces;
using MVC.Dtos;
using MVC.Models.Enums;
using MVC.Services.Interfaces;
using MVC.ViewModels.CatalogViewModels;

namespace MVC.Services;

public class CatalogService : ICatalogService
{
    private readonly IHttpClientService _httpClient;
    private readonly ILogger<CatalogService> _logger;
    private readonly IOptions<AppSettings> _settings;

    public CatalogService(IHttpClientService httpClient, ILogger<CatalogService> logger, IOptions<AppSettings> settings)
    {
        _httpClient = httpClient;
        _settings = settings;
        _logger = logger;
    }

    public async Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? model, int? type, int? subType)
    {
        var filters = new Dictionary<CatalogFilter, int>();

        if (brand.HasValue) filters.Add(CatalogFilter.Brand, brand.Value);

        if (model.HasValue) filters.Add(CatalogFilter.Model, model.Value);

        if (type.HasValue) filters.Add(CatalogFilter.Type, type.Value);

        if (subType.HasValue) filters.Add(CatalogFilter.SubType, subType.Value);

        var result = await _httpClient.SendAsync<Catalog, PaginatedItemsRequest<CatalogFilter>>(
            $"{_settings.Value.CatalogUrl}/items",
            HttpMethod.Post,
            new PaginatedItemsRequest<CatalogFilter>
            {
                PageIndex = page,
                PageSize = take,
                Filters = filters
            });

        return result;
    }

    public async Task<CatalogItem> GetItemById(int id)
    {
        _logger.LogInformation($"item id in service {id}");
        var result = await _httpClient.SendAsync<CatalogItem, int>($"{_settings.Value.CatalogUrl}/GetItemById",
            HttpMethod.Post, id);
        _logger.LogInformation($"Item id from catalog {result.Id}, name {result.Name}");
        return result;
    }

    public async Task<IEnumerable<CatalogBrand>> GetBrands()
    {
        var result =
            await _httpClient.SendAsync<IEnumerable<CatalogBrand>>($"{_settings.Value.CatalogUrl}/GetBrands",
                HttpMethod.Get);
        return result;
    }

    public async Task<IEnumerable<CatalogModel>> GetModelsByBrand(int? selectedBrand)
    {
        return await _httpClient.SendAsync<IEnumerable<CatalogModel>>(
            $"{_settings.Value.CatalogUrl}/GetModels/{selectedBrand}", HttpMethod.Get);
    }

    public async Task<IEnumerable<CatalogType>> GetTypes()
    {
        return await _httpClient.SendAsync<IEnumerable<CatalogType>>($"{_settings.Value.CatalogUrl}/GetTypes",
            HttpMethod.Get);
    }

    public async Task<IEnumerable<CatalogSubType>> GetSubTypesByType(int? selectedType)
    {
        return await _httpClient.SendAsync<IEnumerable<CatalogSubType>>(
            $"{_settings.Value.CatalogUrl}/GetSubTypes/{selectedType}", HttpMethod.Get);
    }
}