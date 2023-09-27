using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Exceptions;

namespace Catalog.Host.Services;

public class CatalogSubTypeService : BaseDataService<ApplicationDbContext>, ICatalogSubTypeService
{
    private readonly ICatalogTypeRepository _catalogTypeRepository;
    private readonly ICatalogSubTypeRepository _catalogSubTypeRepository;

    public CatalogSubTypeService(
        ICatalogTypeRepository catalogTypeRepository,
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogSubTypeRepository catalogSubTypeRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogTypeRepository = catalogTypeRepository;
        _catalogSubTypeRepository = catalogSubTypeRepository;
    }

    public Task<int?> Add(string subTypeName, int typeId)
    {
        var type = ExecuteSafeAsync(() => _catalogTypeRepository.CheckTypeExist(typeId));
        if (type == null)
        {
            throw new BusinessException($"Type with id: {typeId} not found");
        }

        var subType = new CatalogSubType() { SubType = subTypeName, CatalogTypeId = type.Result.Id };
        return ExecuteSafeAsync(() => _catalogSubTypeRepository.Add(subType));
    }

    public Task<int?> Update(int id, string subTypeName, int typeId)
    {
        var subType = ExecuteSafeAsync(() => _catalogSubTypeRepository.CheckSubTypeExist(id));
        if (subType == null)
        {
            throw new BusinessException($"SubType with id: {id} not found");
        }

        subType.Result.SubType = subTypeName;
        subType.Result.CatalogTypeId = typeId;

        return ExecuteSafeAsync(() => _catalogSubTypeRepository.Update(subType.Result));
    }

    public Task<int?> Delete(int id)
    {
        var subType = ExecuteSafeAsync(() => _catalogSubTypeRepository.CheckSubTypeExist(id));
        if (subType == null)
        {
            throw new BusinessException($"SubType with id: {id} not found");
        }

        return ExecuteSafeAsync(() => _catalogSubTypeRepository.Delete(subType.Result));
    }
}