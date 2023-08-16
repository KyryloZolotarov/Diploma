namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogItemService
    {
        Task<int?> Add(int id, string typeName);
        Task<int?> Update(int id, string typeName);
        Task<int?> Delete(int id);
    }
}
