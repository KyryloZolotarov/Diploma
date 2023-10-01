using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Requests;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Exceptions;

namespace Catalog.Host.Repositories;

public class CatalogItemRepository : ICatalogItemRepository
{
    private readonly ApplicationDbContext _dbContext;
    private readonly ILogger<CatalogItemRepository> _logger;

    public CatalogItemRepository(
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<CatalogItemRepository> logger)
    {
        _dbContext = dbContextWrapper.DbContext;
        _logger = logger;
    }

    public async Task<PaginatedItems<CatalogItem>> GetByPageAsync(int pageIndex, int pageSize, int? brandFilter, int? typeFilter, int? subTypeFilter, int? modelFilter)
    {
        IQueryable<CatalogItem> query = _dbContext.CatalogItems;

        if (brandFilter.HasValue)
        {
            query = query.Where(w => w.CatalogModel.CatalogBrandId == brandFilter.Value);
        }

        if (typeFilter.HasValue)
        {
            query = query.Where(w => w.CatalogSubType.CatalogTypeId == typeFilter.Value);
        }

        if (subTypeFilter.HasValue)
        {
            query = query.Where(w => w.CatalogSubTypeId == subTypeFilter.Value);
        }

        if (modelFilter.HasValue)
        {
            query = query.Where(w => w.CatalogModelId == modelFilter.Value);
        }

        var totalItems = await query.LongCountAsync();

        var itemsOnPage = await query.OrderBy(c => c.Name)
            .Include(i => i.CatalogSubType)
            .Include(i => i.CatalogSubType.CatalogType)
            .Include(i => i.CatalogModel)
            .Include(i => i.CatalogModel.CatalogBrand)
            .Skip(pageSize * pageIndex)
            .Take(pageSize)
            .ToListAsync();

        return new PaginatedItems<CatalogItem> { TotalCount = totalItems, Data = itemsOnPage };
    }

    public Task<CatalogItem> GetByIdAsync(int id)
    {
        return _dbContext.CatalogItems
            .Include(i => i.CatalogSubType)
            .Include(i => i.CatalogSubType)
            .Include(i => i.CatalogModel)
            .Include(i => i.CatalogModel.CatalogBrand)
            .FirstAsync(h => h.Id == id);
    }

    public async Task<BasketItems<CatalogItem>> GetItemsListAsync(List<int> basketItemIds)
    {
        var items = await _dbContext.CatalogItems.Include(i => i.CatalogSubType)
                .Include(i => i.CatalogSubType)
                .Include(i => i.CatalogModel)
                .Include(i => i.CatalogModel.CatalogBrand)
                .Where(h => basketItemIds.Contains(h.Id)).ToListAsync();
        return new BasketItems<CatalogItem> { Items = items };
    }

    public async Task<int?> Add(CatalogItem item)
    {
        await _dbContext.AddAsync(item);
        await _dbContext.SaveChangesAsync();
        return item.Id;
    }

    public async Task<int?> Update(CatalogItem item)
    {
        _dbContext.Update(item);
        await _dbContext.SaveChangesAsync();
        return item.Id;
    }

    public async Task<int?> Delete(CatalogItem item)
    {
        _dbContext.Remove(item);
        await _dbContext.SaveChangesAsync();
        return item.Id;
    }

    public async Task<List<CatalogType>> GetTypesAsync()
    {
        return await _dbContext.CatalogItems
            .Select(h => h.CatalogSubType.CatalogType)
            .Distinct()
            .ToListAsync();
    }

    public async Task<List<CatalogBrand>> GetBrandsAsync()
    {
        return await _dbContext.CatalogItems
            .Select(h => h.CatalogModel.CatalogBrand)
            .Distinct()
            .ToListAsync();
    }

    public async Task<IEnumerable<CatalogSubType>> GetSubTypesAsync(int id)
    {
        return await _dbContext.CatalogItems
            .Select(h => h.CatalogSubType)
            .Where(x => x.CatalogTypeId == id)
            .Distinct()
            .ToListAsync();
    }

    public async Task<IEnumerable<CatalogModel>> GetModelsAsync(int id)
    {
        return await _dbContext.CatalogItems
            .Select(h => h.CatalogModel)
            .Where(x => x.CatalogBrandId == id)
            .Distinct()
            .ToListAsync();
    }

    public async Task<CatalogItem> CheckItemExist(int id)
    {
        return await _dbContext.CatalogItems.FirstOrDefaultAsync(h => h.Id == id);
    }
}