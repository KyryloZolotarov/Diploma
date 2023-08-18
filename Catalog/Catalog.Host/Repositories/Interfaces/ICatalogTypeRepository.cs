namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> Add(string typeName);
        Task<int?> Update(int id, string typeName);
        Task<int?> Delete(int id);
    }
}
