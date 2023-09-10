namespace Catalog.Host.Services.Interfaces;

public interface ICatalogModelService
{
    Task<int?> Add(string modelName, int brandId);
    Task<int?> Update(int id, string modelName, int brandId);
    Task<int?> Delete(int id);
}