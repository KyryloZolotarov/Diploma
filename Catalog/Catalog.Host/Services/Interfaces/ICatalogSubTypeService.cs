namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogSubTypeService
    {
        Task<int?> Add(int id, string subTypeName, int typeId);
        Task<int?> Update(int id, string subTypeName, int typeId);
        Task<int?> Delete(int id);
    }
}
