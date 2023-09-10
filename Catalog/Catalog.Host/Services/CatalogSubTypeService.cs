using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services;

public class CatalogSubTypeService : BaseDataService<ApplicationDbContext>, ICatalogSubTypeService
{
    private readonly ICatalogSubTypeRepository _catalogSubTypeRepository;

    public CatalogSubTypeService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogSubTypeRepository catalogSubTypeRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogSubTypeRepository = catalogSubTypeRepository;
    }

    public Task<int?> Add(string subTypeName, int typeId)
    {
        return ExecuteSafeAsync(() => _catalogSubTypeRepository.Add(subTypeName, typeId));
    }

    public Task<int?> Update(int id, string subTypeName, int typeId)
    {
        return ExecuteSafeAsync(() => _catalogSubTypeRepository.Update(id, subTypeName, typeId));
    }

    public Task<int?> Delete(int id)
    {
        return ExecuteSafeAsync(() => _catalogSubTypeRepository.Delete(id));
    }
}