using Catalog.Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Repositories.Intefaces
{
    public interface ICatalogModelRepository
    {
        Task<int?> Add(CatalogModelDto model);
        Task<int?> Update(CatalogModelDto model);
        Task<int?> Delete(CatalogModelDto model);
    }
}
