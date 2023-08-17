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
            var id = await _dbContext.CatalogBrands.MaxAsync(b => b.Id);
            var item = await _dbContext.AddAsync(new CatalogBrand
            {
                Id = id++,
                Brand = brandName
            });

            await _dbContext.SaveChangesAsync();

            return item.Entity.Id;
        }

        public async Task<int?> Update(int id, string brandName)
        {
            var itemExists = await _dbContext.CatalogBrands.AnyAsync(x => x.Id == id);
            switch (itemExists)
            {
                case true:
                    var item = _dbContext.Update(new CatalogBrand
                    {
                        Id = id,
                        Brand = brandName
                    });
                    await _dbContext.SaveChangesAsync();
                    return item.Entity.Id;
                case false: throw new BusinessException($"Brand id: {id} was not founded");
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
                return brandDelete.Id;
            }
            else
            {
                throw new BusinessException($"Brand id: {id} was not founded");
            }
        }
    }
}
