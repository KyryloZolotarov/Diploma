using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Exceptions;

namespace Catalog.Host.Repositories;

public class CatalogSubTypeRepository : ICatalogSubTypeRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogSubTypeRepository> _logger;

    public CatalogSubTypeRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogSubTypeRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> Add(CatalogSubType subType)
    {
        await _dbContext.AddAsync(subType);
        await _dbContext.SaveChangesAsync();
        return subType.Id;
    }

    public async Task<int?> Update(CatalogSubType subType)
    {
        _dbContext.Update(subType);
        await _dbContext.SaveChangesAsync();
        return subType.Id;
    }

    public async Task<int?> Delete(CatalogSubType subType)
    {
        _dbContext.Remove(subType);
        await _dbContext.SaveChangesAsync();
        return subType.Id;
    }

    public async Task<CatalogSubType> CheckSubTypeExist(int id)
    {
        return await _dbContext.CatalogSubTypes.FirstOrDefaultAsync(h => h.Id == id);
    }
}