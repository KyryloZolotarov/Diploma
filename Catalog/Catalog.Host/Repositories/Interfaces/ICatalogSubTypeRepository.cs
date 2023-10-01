using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogSubTypeRepository
{
    Task<int?> Add(CatalogSubType subType);
    Task<int?> Update(CatalogSubType subType);
    Task<int?> Delete(CatalogSubType subType);
    Task<CatalogSubType> CheckSubTypeExist(int id);
}