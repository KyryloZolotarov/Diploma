using MVC.ViewModels;

namespace MVC.Services.Interfaces;

public interface ICatalogService
{
    Task<Catalog> GetCatalogItems(int page, int take, int? brand, int? model, int? type, int? subType);
    Task<IEnumerable<CatalogBrand>> GetBrands();
    Task<IEnumerable<CatalogModel>> GetModels();
    Task<IEnumerable<CatalogType>> GetTypes();
    Task<IEnumerable<CatalogSubType>> GetSubTypes();
}
