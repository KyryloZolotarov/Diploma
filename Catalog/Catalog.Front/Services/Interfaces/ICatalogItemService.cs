using Catalog.Front.Models.Dtos;
using Catalog.Front.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Services.Interfaces
{
    public interface ICatalogItemService
    {
        Task<int?> Add(string name, string description, decimal price, int availableStock, string pictureFileName, int subTypeId, int modelId, string partNumber);

        Task<int?> Update(int id, string name, string description, decimal price, int availableStock, string pictureFileName, int subTypeId, int modelId, string partNumber);

        Task<int?> Delete(int id);
        Task<ObservableCollection<CatalogItemDto>> GetItemsAsync();
    }
}
