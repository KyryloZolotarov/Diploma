using Catalog.Host.Data.Entities;

namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogBrandRepository
{
    Task<int?> Add(CatalogBrand brand);
    Task<int?> Update(CatalogBrand brand);
    Task<int?> Delete(CatalogBrand brand);
    Task<CatalogBrand> CheckBrandExist(int id);
}