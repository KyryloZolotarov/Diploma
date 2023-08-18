namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {
        Task<int?> Add(string typeName);
        Task<int?> Update(int id, string typeName);
        Task<int?> Delete(int id);
    }
}
