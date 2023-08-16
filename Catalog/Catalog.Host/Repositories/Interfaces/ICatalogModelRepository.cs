namespace Catalog.Host.Repositories.Interfaces
{
    public interface ICatalogModelRepository
    {
        Task<int?> Add(int id, string modelName);
        Task<int?> Update(int id, string ModelName);
        Task<int?> Delete(int id);
    }
}
