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

    public async Task<int?> Add(string subTypeName, int typeId)
    {
        var subTypeStatus = await _dbContext.CatalogTypes.AnyAsync(h => h.Id == typeId);
        if (subTypeStatus)
        {
            var subType = await _dbContext.AddAsync(new CatalogSubType
            {
                SubType = subTypeName,
                CatalogTypeId = typeId
            });
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"SubType {subType.Entity.SubType} id: {subType.Entity.Id} added");
            return subType.Entity.Id;
        }

        throw new BusinessException($"Type with Id: {typeId} was not found");
    }

    public async Task<int?> Update(int id, string subTypeName, int typeId)
    {
        var subTypeStatus = await _dbContext.CatalogTypes.AnyAsync(h => h.Id == typeId);
        if (subTypeStatus)
        {
            var subType = _dbContext.Update(new CatalogSubType
            {
                SubType = subTypeName,
                CatalogTypeId = typeId
            });
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"SubType {subType.Entity.SubType} id: {subType.Entity.Id} updated");
            return subType.Entity.Id;
        }

        throw new BusinessException($"Type with Id: {typeId.ToString()} was not found");
    }

    public async Task<int?> Delete(int id)
    {
        var subTypeExists = await _dbContext.CatalogSubTypes.AnyAsync(x => x.Id == id);
        if (subTypeExists)
        {
            var subTypeDelete = await _dbContext.CatalogSubTypes.FirstAsync(h => h.Id == id);
            _dbContext.Remove(subTypeDelete);
            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"SubType {subTypeDelete.SubType} id: {subTypeDelete.Id} deleted");
            return subTypeDelete.Id;
        }

        throw new BusinessException($"SubType id: {id} was not founded");
    }
}