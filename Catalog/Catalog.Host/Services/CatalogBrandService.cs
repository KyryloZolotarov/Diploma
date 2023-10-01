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

    public async Task<int?> Add(string brandName)
    {
        var brand = new CatalogBrand() { Brand = brandName };
        return await ExecuteSafeAsync(() => _catalogBrandRepository.Add(brand));
    }

    public async Task<int?> Update(int id, string brandName)
    {
        var brand = await ExecuteSafeAsync(() => _catalogBrandRepository.CheckBrandExist(id));
        if (brand == null)
        {
            throw new BusinessException($"Brand with id: {id} not found");
        }

        brand.Brand = brandName;

        return await ExecuteSafeAsync(() => _catalogBrandRepository.Update(brand));
    }

    public async Task<int?> Delete(int id)
    {
        var brand = await ExecuteSafeAsync(() => _catalogBrandRepository.CheckBrandExist(id));
        if (brand == null)
        {
            throw new BusinessException($"Brand with id: {id} not found");
        }

        return await ExecuteSafeAsync(() => _catalogBrandRepository.Delete(brand));
    }
}