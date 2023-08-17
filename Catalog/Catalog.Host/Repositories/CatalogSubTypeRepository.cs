using System;
using System.Data;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc.ModelBinding;

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

        public async Task<int?> Add(int id, string subTypeName, int typeId)
        {
            var subTypeStatus = await _dbContext.CatalogTypes.AnyAsync(h => h.Id == typeId);
            switch (subTypeStatus)
            {
                case false:
                    throw new BusinessException($"Type with Id: {typeId.ToString()} was not found");
                case true:
                    var subType = await _dbContext.AddAsync(new CatalogModel
                    {
                        Id = id,
                        Model = subTypeName,
                        CatalogBrandId = typeId
                    });
                    await _dbContext.SaveChangesAsync();
                    return subType.Entity.Id;
            }
        }

        public async Task<int?> Update(int id, string subTypeName, int typeId)
        {
            var subTypeStatus = await _dbContext.CatalogTypes.AnyAsync(h => h.Id == typeId);
            switch (subTypeStatus)
            {
                case false:
                    throw new BusinessException($"Type with Id: {typeId.ToString()} was not found");
                case true:
                    var subType = _dbContext.Update(new CatalogModel
                    {
                        Id = id,
                        Model = subTypeName,
                        CatalogBrandId = typeId
                    });
                    await _dbContext.SaveChangesAsync();
                    return subType.Entity.Id;
            }
        }

        public async Task<int?> Delete(int id)
        {
            var subTypeExists = await _dbContext.CatalogSubTypes.AnyAsync(x => x.Id == id);
            if (subTypeExists == true)
            {
                var subTypeDelete = await _dbContext.CatalogSubTypes.FirstAsync(h => h.Id == id);
                _dbContext.Remove(subTypeDelete);
                await _dbContext.SaveChangesAsync();
                return subTypeDelete.Id;
            }
            else
            {
                throw new BusinessException($"SubType id: {id} was not founded");
            }
        }
    }
}
