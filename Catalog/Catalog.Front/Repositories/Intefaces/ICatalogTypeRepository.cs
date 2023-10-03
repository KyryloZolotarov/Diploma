using Catalog.Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Repositories.Intefaces
{
    public interface ICatalogTypeRepository
    {
        Task<int?> Add(CatalogTypeDto type);
        Task<int?> Update(CatalogTypeDto type);
        Task<int?> Delete(CatalogTypeDto type);
    }
}
