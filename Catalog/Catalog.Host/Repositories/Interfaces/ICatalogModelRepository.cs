using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogModelRepository
{
    Task<int?> Add(CatalogModel model);
    Task<int?> Update(CatalogModel model);
    Task<int?> Delete(CatalogModel model);
    Task<CatalogModel> CheckModelExist(int id);
    Task<IEnumerable<CatalogModel>> GetModelsForOrderAsync(CatalogModelsForOrderRequest modelIds);
}