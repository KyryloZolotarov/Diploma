using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
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
                query = query.Where(w => w.CatalogBrandId == brandFilter.Value);
            }

            if (typeFilter.HasValue)
            {
                query = query.Where(w => w.CatalogTypeId == typeFilter.Value);
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
                .Include(i => i.CatalogModel)
               .Include(i => i.CatalogBrand)
               .Include(i => i.CatalogType)
               .Skip(pageSize * pageIndex)
               .Take(pageSize)
               .ToListAsync();

            return new PaginatedItems<CatalogItem>() { TotalCount = totalItems, Data = itemsOnPage };
        }

        public Task<CatalogItem> GetByIdAsync(int id)
        {
            return _dbContext.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .Include(i => i.CatalogSubType)
                .Include(i => i.CatalogModel)
                .FirstAsync(h => h.Id == id);
        }

        public async Task<int?> Add(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName, int catalogSubTypeId, int catalogModelId, string partNumber)
        {
            var item1 = new CatalogItem
            {
                CatalogBrandId = catalogBrandId,
                CatalogTypeId = catalogTypeId,
                Description = description,
                Name = name,
                PictureFileName = pictureFileName,
                Price = price,
                AvailableStock = availableStock,
                CatalogModelId = catalogModelId,
                CatalogSubTypeId = catalogSubTypeId,
                PartNumber = partNumber
            };
            var item = await _dbContext.AddAsync(item1);

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<int?> Update(string name, string description, decimal price, int availableStock, int catalogBrandId, int catalogTypeId, string pictureFileName, int catalogSubTypeId, int catalogModelId, string partNumber)
        {
            var item = _dbContext.Update(new CatalogItem
            {
                CatalogBrandId = catalogBrandId,
                CatalogTypeId = catalogTypeId,
                Description = description,
                Name = name,
                PictureFileName = pictureFileName,
                Price = price,
                AvailableStock = availableStock,
                CatalogModelId = catalogModelId,
                CatalogSubTypeId = catalogSubTypeId,
                PartNumber = partNumber
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<int?> Delete(int id)
        {
            var itemDelete = await _dbContext.CatalogItems.FirstAsync(h => h.Id == id);
            var item = _dbContext.Remove(itemDelete);
            await _dbContext.SaveChangesAsync();
            return itemDelete.Id;
        }

        public async Task<IEnumerable<CatalogType>> GetTypesAsync()
        {
            return await _dbContext.CatalogItems
                .Select(h => h.CatalogType)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<CatalogBrand>> GetBrandsAsync()
        {
            return await _dbContext.CatalogItems
                .Select(h => h.CatalogBrand)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<CatalogSubType>> GetSubTypesAsync()
        {
            return await _dbContext.CatalogItems
                .Select(h => h.CatalogSubType)
                .Distinct()
                .ToListAsync();
        }

        public async Task<IEnumerable<CatalogModel>> GetModelsAsync()
        {
            return await _dbContext.CatalogItems
                .Select(h => h.CatalogModel)
                .Distinct()
                .ToListAsync();
        }
    }
}
