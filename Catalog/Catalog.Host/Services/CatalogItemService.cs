using Catalog.Host.Data;
using Catalog.Host.Data.Entities;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;
using Infrastructure.Exceptions;

namespace Catalog.Host.Services;

public class CatalogItemService : BaseDataService<ApplicationDbContext>, ICatalogItemService
{
    private readonly ICatalogSubTypeRepository _catalogSubTypeRepository;
    private readonly ICatalogModelRepository _catalogModelRepository;
    private readonly ICatalogItemRepository _catalogItemRepository;

    public CatalogItemService(
        ICatalogSubTypeRepository catalogSubTypeRepository,
        ICatalogModelRepository catalogModelRepository,
        IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
        ILogger<BaseDataService<ApplicationDbContext>> logger,
        ICatalogItemRepository catalogItemRepository)
        : base(dbContextWrapper, logger)
    {
        _catalogSubTypeRepository = catalogSubTypeRepository;
        _catalogModelRepository = catalogModelRepository;
        _catalogItemRepository = catalogItemRepository;
    }

    public Task<int?> Add(string name, string description, decimal price, int availableStock, string pictureFileName, int subTypeId, int modelId, string partNumber)
    {
        var subType = ExecuteSafeAsync(() => _catalogSubTypeRepository.CheckSubTypeExist(subTypeId));
        if (subType == null)
        {
            throw new BusinessException($"SubType with id: {subTypeId} not found");
        }

        var model = ExecuteSafeAsync(() => _catalogModelRepository.CheckModelExist(modelId));
        if (model == null)
        {
            throw new BusinessException($"Model with id: {modelId} not found");
        }

        var item = new CatalogItem()
        {
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price,
            AvailableStock = availableStock,
            CatalogModelId = modelId,
            CatalogSubTypeId = subTypeId,
            PartNumber = partNumber
        };

        return ExecuteSafeAsync(() => _catalogItemRepository.Add(item));
    }

    public Task<int?> Update(int id, string name, string description, decimal price, int availableStock, string pictureFileName, int subTypeId, int modelId, string partNumber)
    {
        var item = ExecuteSafeAsync(() => _catalogItemRepository.CheckItemExist(id));
        if (item == null)
        {
            throw new BusinessException($"Item with id: {id} not found");
        }

        var itemUpdating = new CatalogItem()
        {
            Description = description,
            Name = name,
            PictureFileName = pictureFileName,
            Price = price,
            AvailableStock = availableStock,
            CatalogModelId = modelId,
            CatalogSubTypeId = subTypeId,
            PartNumber = partNumber
        };

        return ExecuteSafeAsync(() => _catalogItemRepository.Update(itemUpdating));
    }

    public Task<int?> Delete(int id)
    {
        var item = ExecuteSafeAsync(() => _catalogItemRepository.CheckItemExist(id));
        if (item == null)
        {
            throw new BusinessException($"Item with id: {id} not found");
        }

        return ExecuteSafeAsync(() => _catalogItemRepository.Delete(item.Result));
    }
}