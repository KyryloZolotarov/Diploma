namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogSubTypeService
    {

        Task<int?> Add(int id, string subTypeName);
        Task<int?> Update(int id, string subTypeName);
        Task<int?> Delete(int id);
    }
}
