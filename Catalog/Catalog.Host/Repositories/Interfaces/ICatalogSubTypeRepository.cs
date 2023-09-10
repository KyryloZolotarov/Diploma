namespace Catalog.Host.Repositories.Interfaces;

public interface ICatalogSubTypeRepository
{
    Task<int?> Add(string subTypeName, int typeId);
    Task<int?> Update(int id, string subTypeName, int typeId);
    Task<int?> Delete(int id);
}