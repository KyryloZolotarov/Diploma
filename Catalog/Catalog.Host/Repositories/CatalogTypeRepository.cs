using System.Data;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Exceptions;

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

        public async Task<int?> Add(string typeName)
        {
            var type = await _dbContext.AddAsync(new CatalogType
            {
                Type = typeName
            });

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Type {type.Entity.Type} id: {type.Entity.Id} added");
            return type.Entity.Id;
        }

        public async Task<int?> Update(int id, string typeName)
        {
            var typeExists = await _dbContext.CatalogTypes.AnyAsync(x => x.Id == id);
            if (typeExists == true)
            {
                var type = _dbContext.Update(new CatalogType
                {
                    Id = id,
                    Type = typeName
                });
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Type {type.Entity.Type} id: {type.Entity.Id} updated");
                return type.Entity.Id;
            }
            else
            {
                throw new BusinessException($"Type id: {id} was not founded");
            }
        }

        public async Task<int?> Delete(int id)
        {
            var typeExists = await _dbContext.CatalogTypes.AnyAsync(x => x.Id == id);
            if (typeExists == true)
            {
                var typeDelete = await _dbContext.CatalogTypes.FirstAsync(h => h.Id == id);
                _dbContext.Remove(typeDelete);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Type {typeDelete.Type} id: {typeDelete.Id} deleted");
                return typeDelete.Id;
            }
            else
            {
                throw new BusinessException($"Type id: {id} was not founded");
            }
        }
    }
}
