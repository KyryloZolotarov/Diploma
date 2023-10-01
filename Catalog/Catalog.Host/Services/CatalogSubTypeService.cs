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

    public async Task<int?> Add(string subTypeName, int typeId)
    {
        var type = await ExecuteSafeAsync(() => _catalogTypeRepository.CheckTypeExist(typeId));
        if (type == null)
        {
            throw new BusinessException($"Type with id: {typeId} not found");
        }

        var subType = new CatalogSubType() { SubType = subTypeName, CatalogTypeId = type.Id };
        return await ExecuteSafeAsync(() => _catalogSubTypeRepository.Add(subType));
    }

    public async Task<int?> Update(int id, string subTypeName, int typeId)
    {
        var subType = await ExecuteSafeAsync(() => _catalogSubTypeRepository.CheckSubTypeExist(id));
        if (subType == null)
        {
            throw new BusinessException($"SubType with id: {id} not found");
        }

        subType.SubType = subTypeName;
        subType.CatalogTypeId = typeId;

        return await ExecuteSafeAsync(() => _catalogSubTypeRepository.Update(subType));
    }

    public async Task<int?> Delete(int id)
    {
        var subType = await ExecuteSafeAsync(() => _catalogSubTypeRepository.CheckSubTypeExist(id));
        if (subType == null)
        {
            throw new BusinessException($"SubType with id: {id} not found");
        }

        return await ExecuteSafeAsync(() => _catalogSubTypeRepository.Delete(subType));
    }
}