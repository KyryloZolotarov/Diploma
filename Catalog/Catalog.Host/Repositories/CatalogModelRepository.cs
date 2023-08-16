using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;

namespace Catalog.Host.Repositories
{
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

        public async Task<int?> Add(int id, string modelName)
        {
            var model = await _dbContext.AddAsync(new CatalogModel
            {
                Id = id,
                Model = modelName
            });

            await _dbContext.SaveChangesAsync();

            return model.Entity.Id;
        }

        public async Task<int?> Update(int id, string modelName)
        {
            var model = _dbContext.Update(new CatalogModel
            {
                Id = id,
                Model = modelName
            });

            await _dbContext.SaveChangesAsync();

            return model.Entity.Id;
        }

        public async Task<int?> Delete(int id)
        {
            var modelDelete = await _dbContext.CatalogModel.FirstAsync(h => h.Id == id);
            var model = _dbContext.Remove(modelDelete);
            await _dbContext.SaveChangesAsync();
            return modelDelete.Id;
        }
    }
}
