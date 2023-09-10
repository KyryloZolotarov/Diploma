namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogModelRepository
{
    Task<int?> Add(string modelName, int brandId);
    Task<int?> Update(int id, string modelName, int brandId);
    Task<int?> Delete(int id);
}