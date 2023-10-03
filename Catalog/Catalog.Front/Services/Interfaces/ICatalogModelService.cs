using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Services.Interfaces
{
    public interface ICatalogModelService
    {
        Task<int?> Add(string modelName, int brandId);
        Task<int?> Update(int id, string modelName, int brandId);
        Task<int?> Delete(int id);
    }
}
