using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Services.Interfaces
{
    public interface ICatalogBrandService
    {
        Task<int?> Add(string brandName);

        Task<int?> Update(int id, string brandName);

        Task<int?> Delete(int id);
    }
}
