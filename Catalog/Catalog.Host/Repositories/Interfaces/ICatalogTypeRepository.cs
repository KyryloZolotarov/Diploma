namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> Add(int id, string typeName);
        Task<int?> Update(int id, string typeName);
        Task<int?> Delete(int id);
    }
}
