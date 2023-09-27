using System;
using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Exceptions;

namespace Catalog.Host.Services;

public class CatalogModelService : BaseDataService<ApplicationDbContext>, ICatalogModelService
{
    private readonly ICatalogBrandRepository _catalogBrandRepository;
    private readonly ICatalogModelRepository _catalogModelRepository;

    public CatalogModelService(
        ICatalogBrandRepository catalogBrandRepository,
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogModelRepository catalogModelRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogBrandRepository = catalogBrandRepository;
        _catalogModelRepository = catalogModelRepository;
    }

    public Task<int?> Add(string modelName, int brandId)
    {
        var brand = ExecuteSafeAsync(() => _catalogBrandRepository.CheckBrandExist(brandId));
        if (brand == null)
        {
            throw new BusinessException($"Brand with id: {brandId} not found");
        }

        var model = new CatalogModel() { Model = modelName, CatalogBrandId = brandId };
        return ExecuteSafeAsync(() => _catalogModelRepository.Add(model));
    }

    public Task<int?> Update(int id, string modelName, int brandId)
    {
        var model = ExecuteSafeAsync(() => _catalogModelRepository.CheckModelExist(id));
        if (model == null)
        {
            throw new BusinessException($"Model with id: {id} not found");
        }

        model.Result.Model = modelName;
        model.Result.CatalogBrandId = brandId;

        return ExecuteSafeAsync(() => _catalogModelRepository.Update(model.Result));
    }

    public Task<int?> Delete(int id)
    {
        var model = ExecuteSafeAsync(() => _catalogModelRepository.CheckModelExist(id));
        if (model == null)
        {
            throw new BusinessException($"Model with id: {id} not found");
        }

        return ExecuteSafeAsync(() => _catalogModelRepository.Delete(model.Result));
    }
}