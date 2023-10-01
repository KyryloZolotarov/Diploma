using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Exceptions;

namespace Catalog.Host.Repositories;

public class CatalogTypeRepository : ICatalogTypeRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogTypeRepository> _logger;

    public CatalogTypeRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogTypeRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> Add(CatalogType type)
    {
        var addedType = await _dbContext.AddAsync(type);
        await _dbContext.SaveChangesAsync();
        return addedType.Entity.Id;
    }

    public async Task<int?> Update(CatalogType type)
    {
        var typeUpdated = _dbContext.Update(type);
        await _dbContext.SaveChangesAsync();
        return typeUpdated.Entity.Id;
    }

    public async Task<int?> Delete(CatalogType type)
    {
        _dbContext.Remove(type);
        await _dbContext.SaveChangesAsync();
        return type.Id;
    }

    public async Task<CatalogType> CheckTypeExist(int id)
    {
        return await _dbContext.CatalogTypes.FirstOrDefaultAsync(h => h.Id == id);
    }
}