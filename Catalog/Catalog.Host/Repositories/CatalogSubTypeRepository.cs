using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using System.Data;

namespace Catalog.Host.Repositories
{
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

        public async Task<int?> Add(int id, string subTypeName)
        {
            var subType = await _dbContext.AddAsync(new CatalogSubType
            {
                Id = id,
                SubType = subTypeName
            });

            await _dbContext.SaveChangesAsync();

            return subType.Entity.Id;
        }

        public async Task<int?> Update(int id, string subTypeName)
        {
            var subType = _dbContext.Update(new CatalogSubType
            {
                Id = id,
                SubType = subTypeName
            });

            await _dbContext.SaveChangesAsync();

            return subType.Entity.Id;
        }

        public async Task<int?> Delete(int id)
        {
            var subTypeDelete = await _dbContext.CatalogSubType.FirstAsync(h => h.Id == id);
            var subType = _dbContext.Remove(subTypeDelete);
            await _dbContext.SaveChangesAsync();
            return subTypeDelete.Id;
        }
    }
}
