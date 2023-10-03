using Catalog.Front.Models.Dtos;
using Catalog.Front.Repositories.Intefaces;
using Catalog.Front.Services.Interfaces;
using Catalog.Front.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Services
{
    public class CatalogItemService : ICatalogItemService
    {
        private readonly ICatalogItemRepository _catalogItemRepository;

        public CatalogItemService(
            ICatalogItemRepository catalogItemRepository)
        {
            _catalogItemRepository = catalogItemRepository;
        }

        public Task<int?> Add(string name, string description, decimal price, int availableStock, string pictureFileName, int subTypeId, int modelId, string partNumber)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Update(int id, string name, string description, decimal price, int availableStock, string pictureFileName, int subTypeId, int modelId, string partNumber)
        {
            throw new NotImplementedException();
        }

        public async Task<ObservableCollection<CatalogItemDto>> GetItemsAsync()
        {
            var items = await _catalogItemRepository.GetItemsAsync();
            var itemsList = new ObservableCollection<CatalogItemDto>();
            if (items == null) { return null; }
            foreach (var item in items)
            {
                itemsList.Add(item);
            }
            return itemsList;
        }
    }
}
