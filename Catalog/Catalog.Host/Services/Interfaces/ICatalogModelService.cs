namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogModelService
    {

        Task<int?> Add(int id, string modelName);
        Task<int?> Update(int id, string modelName);
        Task<int?> Delete(int id);
    }
}
