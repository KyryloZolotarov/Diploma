using MVC.Dtos;
using MVC.Models.Enums;
using MVC.Models.Requests;
using MVC.Models.Responses;
using MVC.ViewModels.BasketViewModels;
using MVC.ViewModels.CatalogViewModels;

namespace MVC.Repositories.Interfaces
{
    public interface ICatalogRepository
    {
        Task<BasketItemsFromCatalog> GetCatalogItemsForBasket(BasketItemsFromBasket items);
        Task<Catalog> GetCatalogItems(PaginatedItemsRequest<CatalogFilter> items);
        Task<CatalogItem> GetItemById(int id);
        Task<IEnumerable<CatalogBrand>> GetBrands();
        Task<IEnumerable<CatalogModel>> GetModelsByBrand(int? selectedBrand);
        Task<IEnumerable<CatalogType>> GetTypes();
        Task<IEnumerable<CatalogSubType>> GetSubTypesByType(int? selectedType);
        Task<bool> ChangeAvailableItems(ListOrderItemsRequest order);
        Task<CatalogModelsForOrderResponse> GetItemsForOrder(CatalogModelForOrderRequest modelIds);
    }
}
