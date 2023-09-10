namespace Catalog.Host.Services.Interfaces;

public interface ICatalogItemService
{
    Task<int?> Add(string name, string description, decimal price, int availableStock, string pictureFileName, int subTypeId, int modelId, string partNumber);

    Task<int?> Update(int id, string name, string description, decimal price, int availableStock, string pictureFileName, int subTypeId, int modelId, string partNumber);

    Task<int?> Delete(int id);
}