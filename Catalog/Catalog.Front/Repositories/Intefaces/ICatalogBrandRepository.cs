using Catalog.Front.Models.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Repositories.Intefaces
{
    public interface ICatalogBrandRepository
    {
        Task<int?> Add(CatalogBrandDto brand);
        Task<int?> Update(CatalogBrandDto brand);
        Task<int?> Delete(CatalogBrandDto brand);
        Task<CatalogBrandDto> CheckBrandExist(int id);
    }
}
