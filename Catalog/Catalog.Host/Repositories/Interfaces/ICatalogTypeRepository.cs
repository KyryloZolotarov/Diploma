using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogTypeRepository
{
    Task<int?> Add(CatalogType type);
    Task<int?> Update(CatalogType type);
    Task<int?> Delete(CatalogType type);
    Task<CatalogType> CheckTypeExist(int id);
}