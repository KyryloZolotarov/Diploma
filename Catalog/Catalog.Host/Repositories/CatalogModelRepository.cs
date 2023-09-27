using System.Data;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Requests;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Exceptions;

namespace Catalog.Host.Repositories;

public class CatalogModelRepository : ICatalogModelRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogModelRepository> _logger;

    public CatalogModelRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogModelRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<int?> Add(CatalogModel model)
    {
        await _dbContext.AddAsync(model);
        await _dbContext.SaveChangesAsync();
        return model.Id;
    }

    public async Task<int?> Update(CatalogModel model)
    {
        _dbContext.Update(model);
        await _dbContext.SaveChangesAsync();
        return model.Id;
    }

    public async Task<int?> Delete(CatalogModel model)
    {
        _dbContext.Remove(model);
        await _dbContext.SaveChangesAsync();
        return model.Id;
    }

    public async Task<CatalogModel> CheckModelExist(int id)
    {
        return await _dbContext.CatalogModels.FirstOrDefaultAsync(h => h.Id == id);
    }

    public async Task<IEnumerable<CatalogModel>> GetModelsForOrderAsync(CatalogModelsForOrderRequest modelIds)
    {
        var models = await _dbContext.CatalogModels.Where(h => modelIds.Id.Contains(h.Id)).ToListAsync();
        return models;
    }
}