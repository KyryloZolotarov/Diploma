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

    public async Task<int?> Add(string name, string description, decimal price, int availableStock, string pictureFileName, int subTypeId, int modelId, string partNumber)
    {
        var subType = await ExecuteSafeAsync(() => _catalogSubTypeRepository.CheckSubTypeExist(subTypeId));
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

        return await ExecuteSafeAsync(() => _catalogItemRepository.Add(item));
    }

    public async Task<int?> Update(int id, string name, string description, decimal price, int availableStock, string pictureFileName, int subTypeId, int modelId, string partNumber)
    {
        var item = await ExecuteSafeAsync(() => _catalogItemRepository.CheckItemExist(id));
        if (item == null)
        {
            throw new BusinessException($"Item with id: {id} not found");
        }

        item.Description = description;
        item.Price = price;
        item.AvailableStock = availableStock;
        item.PictureFileName = pictureFileName;
        item.Price = price;
        item.PartNumber = partNumber;
        item.CatalogModelId = modelId;
        item.CatalogSubTypeId = subTypeId;

        return await ExecuteSafeAsync(() => _catalogItemRepository.Update(item));
    }

    public async Task<int?> Delete(int id)
    {
        var item = await ExecuteSafeAsync(() => _catalogItemRepository.CheckItemExist(id));
        if (item == null)
        {
            throw new BusinessException($"Item with id: {id} not found");
        }

        return await ExecuteSafeAsync(() => _catalogItemRepository.Delete(item));
    }
}