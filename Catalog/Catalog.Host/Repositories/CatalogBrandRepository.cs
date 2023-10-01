using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Exceptions;

namespace Catalog.Host.Repositories;

public class CatalogBrandRepository : ICatalogBrandRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogBrandRepository> _logger;

    public CatalogBrandRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogBrandRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> Add(CatalogBrand brand)
    {
        await _dbContext.AddAsync(brand);
        await _dbContext.SaveChangesAsync();
        return brand.Id;
    }

    public async Task<int?> Update(CatalogBrand brand)
    {
        _dbContext.Update(brand);
        await _dbContext.SaveChangesAsync();
        return brand.Id;
    }

    public async Task<int?> Delete(CatalogBrand brand)
    {
        _dbContext.Remove(brand);
        await _dbContext.SaveChangesAsync();
        return brand.Id;
    }

    public async Task<CatalogBrand> CheckBrandExist(int id)
    {
        return await _dbContext.CatalogBrands.FirstOrDefaultAsync(h => h.Id == id);
    }
}