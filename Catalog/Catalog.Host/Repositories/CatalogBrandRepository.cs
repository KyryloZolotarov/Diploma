using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
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

        public async Task<int?> Add(int id, string brandName)
        {
            var item = await _dbContext.AddAsync(new CatalogBrand
            {
                Id = id,
                Brand = brandName
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<int?> Update(int id, string brandName)
        {
            var item = _dbContext.Update(new CatalogBrand
            {
                Id = id,
                Brand = brandName
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<int?> Delete(int id)
        {
            var itemDelete = await _dbContext.CatalogItems
                .Include(i => i.CatalogBrand)
                .Include(i => i.CatalogType)
                .FirstAsync(h => h.Id == id);
            var item = _dbContext.Remove(itemDelete);
            await _dbContext.SaveChangesAsync();
            return itemDelete.Id;
        }
    }
}
