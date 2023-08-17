using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Exceptions;

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

        public async Task<int?> Add(string modelName, int brandId)
        {
            var id = await _dbContext.CatalogModels.MaxAsync(b => b.Id);
            var brandStatus = await _dbContext.CatalogBrands.AnyAsync(h => h.Id == brandId);
            switch (brandStatus)
            {
                case false:
                    throw new BusinessException($"Brand with Id: {brandId} was not found");
                case true:
                    var model = await _dbContext.AddAsync(new CatalogModel
                    {
                        Id = id++,
                        Model = modelName,
                        CatalogBrandId = brandId
                    });
                    await _dbContext.SaveChangesAsync();
                    return model.Entity.Id;
            }
        }

        public async Task<int?> Update(int id, string modelName, int brandId)
        {
            var modelExists = await _dbContext.CatalogBrands.AnyAsync(h => h.Id == id);
            if (modelExists == true)
            {
                var brandStatus = await _dbContext.CatalogBrands.AnyAsync(h => h.Id == brandId);
                switch (brandStatus)
                {
                    case false:
                        throw new BusinessException($"Brand with Id: {brandId.ToString()} was not found");
                    case true:
                        var model = _dbContext.Update(new CatalogModel { Id = id, Model = modelName, CatalogBrandId = brandId });
                        await _dbContext.SaveChangesAsync();
                        return model.Entity.Id;
                }
            }
            else
            {
                throw new BusinessException($"Model with Id: {id} was not found");
            }
        }

        public async Task<int?> Delete(int id)
        {
            var modelExists = await _dbContext.CatalogModels.AnyAsync(x => x.Id == id);
            if (modelExists == true)
            {
                var modelDelete = await _dbContext.CatalogModels.FirstAsync(h => h.Id == id);
                _dbContext.Remove(modelDelete);
                await _dbContext.SaveChangesAsync();
                return modelDelete.Id;
            }
            else
            {
                throw new BusinessException($"Model id: {id} was not founded");
            }
        }
    }
}
