using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Services.Interfaces
{
    public interface ICatalogSubTypeService
    {
        Task<int?> Add(string subTypeName, int typeId);
        Task<int?> Update(int id, string subTypeName, int typeId);
        Task<int?> Delete(int id);
    }
}
