using Catalog.Front.Repositories.Intefaces;
using Catalog.Front.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Services
{
    public class CatalogBrandService : ICatalogBrandService
    {
        private readonly ICatalogBrandRepository _catalogBrandRepository;

        public CatalogBrandService(
            ICatalogBrandRepository catalogBrandRepository)
        {
            _catalogBrandRepository = catalogBrandRepository;
        }

        public Task<int?> Add(string brandName)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Update(int id, string brandName)
        {
            throw new NotImplementedException();
        }
    }
}
