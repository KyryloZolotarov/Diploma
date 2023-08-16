namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogSubTypeRepository
    {
        Task<int?> Add(int id, string subTypeName);
        Task<int?> Update(int id, string subTypeName);
        Task<int?> Delete(int id);
    }
}
