using Catalog.Front.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Repositories.Intefaces
{
    public interface ICatalogSubTypeRepository
    {
        Task<int?> Add(CatalogSubTypeDto subType);
        Task<int?> Update(CatalogSubTypeDto subType);
        Task<int?> Delete(CatalogSubTypeDto subType);
    }
}
