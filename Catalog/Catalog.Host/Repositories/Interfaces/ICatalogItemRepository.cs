using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogItemRepository
{
    Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter, int? subTypeFilter, int? modelFilter);

    Task<CatalogItem> GetByIdAsync(int id);
    Task<BasketItems<CatalogItem>> GetItemsListAsync(List<int> basketItemIds);

    Task<int?> Add(CatalogItem item);

    Task<int?> Update(CatalogItem item);

    Task<int?> Delete(CatalogItem item);
    Task<List<CatalogType>> GetTypesAsync();
    Task<List<CatalogBrand>> GetBrandsAsync();
    Task<IEnumerable<CatalogSubType>> GetSubTypesAsync(int id);
    Task<IEnumerable<CatalogModel>> GetModelsAsync(int id);
    Task<CatalogItem> CheckItemExist(int id);
}