using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
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

        public async Task<int?> Add(int id, string typeName)
        {
            var item = await _dbContext.AddAsync(new CatalogType
            {
                Id = id,
                Type = typeName
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<int?> Update(int id, string typeName)
        {
            var item = _dbContext.Update(new CatalogType
            {
                Id = id,
                Type = typeName
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
    }
}
