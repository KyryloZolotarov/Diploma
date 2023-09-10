using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogItemRepository _catalogItemRepository;

    public CatalogItemService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogItemRepository = catalogItemRepository;
    }

    public Task<int?> Add(string name, string description, decimal price, int availableStock, string pictureFileName, int subTypeId, int modelId, string partNumber)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Add(name, description, price, availableStock, pictureFileName, subTypeId, modelId, partNumber));
    }

    public Task<int?> Update(int id, string name, string description, decimal price, int availableStock, string pictureFileName, int subTypeId, int modelId, string partNumber)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Update(id, name, description, price, availableStock, pictureFileName, subTypeId, modelId, partNumber));
    }

    public Task<int?> Delete(int id)
    {
        return ExecuteSafeAsync(() => _catalogItemRepository.Delete(id));
    }
}