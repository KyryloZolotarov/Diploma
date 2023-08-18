using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Exceptions;

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

        public async Task<int?> Add(string brandName)
        {
            var brand = await _dbContext.AddAsync(new CatalogBrand
            {
                Brand = brandName
            });

            await _dbContext.SaveChangesAsync();
            _logger.LogInformation($"Brand {brand.Entity.Brand} id: {brand.Entity.Id} added");
            return brand.Entity.Id;
        }

        public async Task<int?> Update(int id, string brandName)
        {
            var brandExists = await _dbContext.CatalogBrands.AnyAsync(x => x.Id == id);
            if (brandExists == true)
            {
                var brand = _dbContext.Update(new CatalogBrand
                {
                    Id = id,
                    Brand = brandName
                });
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Brand {brand.Entity.Brand} id: {brand.Entity.Id} updated");
                return brand.Entity.Id;
            }
            else
            {
                throw new BusinessException($"Brand id: {id} was not founded");
            }
        }

        public async Task<int?> Delete(int id)
        {
            var brandExists = await _dbContext.CatalogBrands.AnyAsync(x => x.Id == id);
            if (brandExists == true)
            {
                var brandDelete = await _dbContext.CatalogBrands.FirstAsync(h => h.Id == id);
                _dbContext.Remove(brandDelete);
                await _dbContext.SaveChangesAsync();
                _logger.LogInformation($"Brand {brandDelete.Brand} id: {brandDelete.Id} deleted");
                return brandDelete.Id;
            }
            else
            {
                throw new BusinessException($"Brand id: {id} was not founded");
            }
        }
    }
}
