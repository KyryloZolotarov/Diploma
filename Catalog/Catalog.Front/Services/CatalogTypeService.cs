using Catalog.Front.Repositories.Intefaces;
using Catalog.Front.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Services
{
    public class CatalogTypeService : ICatalogTypeService
    {
        public readonly ICatalogTypeRepository _catalogTypeRepository;
        public CatalogTypeService(ICatalogTypeRepository catalogTypeRepository)
        {
            _catalogTypeRepository = catalogTypeRepository;
        }

        public Task<int?> Add(string typeName)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Update(int id, string typeName)
        {
            throw new NotImplementedException();
        }
    }
}
