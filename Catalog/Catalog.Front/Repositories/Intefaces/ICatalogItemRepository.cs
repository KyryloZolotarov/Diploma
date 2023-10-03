using Catalog.Front.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Repositories.Intefaces
{
    public interface ICatalogItemRepository
    {
        Task<int?> Add(CatalogItemDto item);

        Task<int?> Update(CatalogItemDto item);

        Task<int?> Delete(CatalogItemDto item);
        Task<IEnumerable<CatalogItemDto>> GetItemsAsync();
    }
}
