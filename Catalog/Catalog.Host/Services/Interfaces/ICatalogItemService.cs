namespace Catalog.Host.Services.Interfaces
{
    public interface ICatalogItemService
    {
        Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName, int subTypeId, int modelId, string partNumber);
        Task<int?> Update(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName, int subTypeId, int modelId, string partNumber);
        Task<int?> Delete(int id);
    }
}
