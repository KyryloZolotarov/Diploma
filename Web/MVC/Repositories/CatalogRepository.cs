using Infrastructure.Services.Interfaces;
using Microsoft.AspNetCore.Mvc.RazorPages;
using MVC.Dtos;
using MVC.Models.Enums;
using MVC.Models.Requests;
using MVC.Models.Responses;
using MVC.Repositories.Interfaces;
using MVC.ViewModels.BasketViewModels;
using MVC.ViewModels.CatalogViewModels;

namespace MVC.Repositories
{
    public class CatalogRepository : ICatalogRepository
    {
        private readonly IHttpClientService _httpClient;
        private readonly ILogger<CatalogRepository> _logger;
        private readonly IOptions<AppSettings> _settings;

        public CatalogRepository(IHttpClientService httpClient, ILogger<CatalogRepository> logger, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
            _logger = logger;
        }

        public async Task<BasketItemsFromCatalog> GetCatalogItemsForBasket(BasketItemsFromBasket items)
        {
            var result =
            await _httpClient.SendAsync<BasketItemsFromCatalog, BasketItemsFromBasket>(
                $"{_settings.Value.CatalogUrl}/ListItems", HttpMethod.Post, items);
            return result;
        }

        public async Task<Catalog> GetCatalogItems(PaginatedItemsRequest<CatalogFilter> items)
        {
            return await _httpClient.SendAsync<Catalog, PaginatedItemsRequest<CatalogFilter>>(
            $"{_settings.Value.CatalogUrl}/items",
            HttpMethod.Post, items);
        }

        public async Task<CatalogItem> GetItemById(int id)
        {
            return await _httpClient.SendAsync<CatalogItem, int>($"{_settings.Value.CatalogUrl}/GetItemById",
            HttpMethod.Post, id);
        }
        public async Task<IEnumerable<CatalogBrand>> GetBrands()
        {
            return await _httpClient.SendAsync<IEnumerable<CatalogBrand>>($"{_settings.Value.CatalogUrl}/GetBrands",
                    HttpMethod.Get);
        }
        public async Task<IEnumerable<CatalogModel>> GetModelsByBrand(int? selectedBrand)
        {
            return await _httpClient.SendAsync<IEnumerable<CatalogModel>>(
                $"{_settings.Value.CatalogUrl}/GetModels/{selectedBrand}", HttpMethod.Get);
        }
        public async Task<IEnumerable<CatalogType>> GetTypes()
        {
            return await _httpClient.SendAsync<IEnumerable<CatalogType>>($"{_settings.Value.CatalogUrl}/GetTypes",
                HttpMethod.Get);
        }
        public async Task<IEnumerable<CatalogSubType>> GetSubTypesByType(int? selectedType)
        {
            return await _httpClient.SendAsync<IEnumerable<CatalogSubType>>(
                $"{_settings.Value.CatalogUrl}/GetSubTypes/{selectedType}", HttpMethod.Get);
        }

        public async Task<bool> ChangeAvailableItems(ListOrderItemsRequest order)
        {
            return await _httpClient.SendAsync<bool, ListOrderItemsRequest>(
                $"{_settings.Value.CatalogUrl}/ChangeAvailableItems", HttpMethod.Post, order);
        }

        public async Task<CatalogModelsForOrderResponse> GetItemsForOrder(CatalogModelForOrderRequest modelIds)
        {
            return await _httpClient.SendAsync<CatalogModelsForOrderResponse, CatalogModelForOrderRequest>(
            $"{_settings.Value.CatalogUrl}/GetModelsForOrder", HttpMethod.Post, modelIds);
        }
    }
}
