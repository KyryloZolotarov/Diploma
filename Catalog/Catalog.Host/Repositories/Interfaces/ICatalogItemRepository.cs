using Catalog.Host.Data;
using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogItemRepository
    {
        Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter, int? subTypeFilter, int? modelFilter);
        Task<CatalogItem> GetByIdAsync(int id);
        Task<int?> Add(string name, string description, decimal price, int availableStock, string pictureFileName, int catalogSubTypeId, int catalogModelId, string partNumber);
        Task<int?> Update(string name, string description, decimal price, int availableStock, string pictureFileName, int catalogSubTypeId, int catalogModelId, string partNumber);
        Task<int?> Delete(int id);
        Task<IEnumerable<CatalogType>> GetTypesAsync();
        Task<IEnumerable<CatalogBrand>> GetBrandsAsync();
        Task<IEnumerable<CatalogSubType>> GetSubTypesAsync();
        Task<IEnumerable<CatalogModel>> GetModelsAsync();
    }
}
