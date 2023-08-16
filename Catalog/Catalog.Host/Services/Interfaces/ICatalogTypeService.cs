namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogTypeService
    {

        Task<int?> Add(int id, string typeName);
        Task<int?> Update(int id, string typeName);
        Task<int?> Delete(int id);
    }
}
