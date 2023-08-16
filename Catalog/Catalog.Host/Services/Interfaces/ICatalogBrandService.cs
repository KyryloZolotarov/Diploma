namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<int?> Add(int id, string brandName);

        Task<int?> Update(int id, string brandName);

        Task<int?> Delete(int id);
    }
}
