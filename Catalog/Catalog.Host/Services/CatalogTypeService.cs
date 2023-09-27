using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Catalog.Host.Services;

public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
{
    private readonly ICatalogTypeRepository _catalogTypeRepository;

    public CatalogTypeService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogTypeRepository catalogTypeRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogTypeRepository = catalogTypeRepository;
    }

    public async Task<int?> Add(string typeName)
    {
        var type = new CatalogType() { Type = typeName };
        return await ExecuteSafeAsync(() => _catalogTypeRepository.Add(type));
    }

    public async Task<int?> Update(int id, string typeName)
    {
        var type = await ExecuteSafeAsync(() => _catalogTypeRepository.CheckTypeExist(id));
        if (type == null)
        {
            throw new BusinessException($"Type with id: {id} not found");
        }

        return await ExecuteSafeAsync(() => _catalogTypeRepository.Update(type));
    }

    public async Task<int?> Delete(int id)
    {
        var type = await ExecuteSafeAsync(() => _catalogTypeRepository.CheckTypeExist(id));
        if (type == null)
        {
            throw new BusinessException($"Type with id: {id} not found");
        }

        return await ExecuteSafeAsync(() => _catalogTypeRepository.Delete(type));
    }
}