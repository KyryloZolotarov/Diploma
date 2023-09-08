using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests.UpdateRequsts;

namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogItemRepository
    {
        Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter, int? subTypeFilter, int? modelFilter);
        Task<CatalogItem> GetByIdAsync(int id);
        Task<BasketItems<CatalogItem>> GetItemsListAsync(List<BasketItemDto> items);
        Task<int?> Add(string name, string description, decimal price, int availableStock, string pictureFileName, int catalogSubTypeId, int catalogModelId, string partNumber);
        Task<int?> Update(int id, string name, string description, decimal price, int availableStock, string pictureFileName, int catalogSubTypeId, int catalogModelId, string partNumber);
        Task<int?> Delete(int id);
        Task<List<CatalogType>> GetTypesAsync();
        Task<List<CatalogBrand>> GetBrandsAsync();
        Task<IEnumerable<CatalogSubType>> GetSubTypesAsync(int id);
        Task<IEnumerable<CatalogModel>> GetModelsAsync(int id);
        Task<bool> ChangeAvailableItems(int id, int count);
    }
}
