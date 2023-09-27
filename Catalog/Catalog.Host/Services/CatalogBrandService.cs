using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Exceptions;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace Catalog.Host.Services;

public class CatalogBrandService : BaseDataService<ApplicationDbContext>, ICatalogBrandService
{
    private readonly ICatalogBrandRepository _catalogBrandRepository;

    public CatalogBrandService(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogBrandRepository catalogBrandRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogBrandRepository = catalogBrandRepository;
    }

    public Task<int?> Add(string brandName)
    {
        var brand = new CatalogBrand() { Brand = brandName };
        return ExecuteSafeAsync(() => _catalogBrandRepository.Add(brand));
    }

    public Task<int?> Update(int id, string brandName)
    {
        var brand = ExecuteSafeAsync(() => _catalogBrandRepository.CheckBrandExist(id));
        if (brand == null)
        {
            throw new BusinessException($"Brand with id: {id} not found");
        }

        brand.Result.Brand = brandName;

        return ExecuteSafeAsync(() => _catalogBrandRepository.Update(brand.Result));
    }

    public Task<int?> Delete(int id)
    {
        var brand = ExecuteSafeAsync(() => _catalogBrandRepository.CheckBrandExist(id));
        if (brand == null)
        {
            throw new BusinessException($"Brand with id: {id} not found");
        }

        return ExecuteSafeAsync(() => _catalogBrandRepository.Delete(brand.Result));
    }
}