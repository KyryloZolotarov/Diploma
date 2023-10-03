using Catalog.Front.Helpers.Interfaces;
using Catalog.Front.Models.Dtos;
using Catalog.Front.Repositories.Intefaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Repositories
{
    public class CatalogBrandRepository : ICatalogBrandRepository
    {
        private readonly IHttpClientHelper _httpClient;
        private readonly IOptions<AppSettings> _settings;

        public CatalogBrandRepository(IHttpClientHelper httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public Task<int?> Add(CatalogBrandDto brand)
        {
            throw new NotImplementedException();
        }

        public Task<CatalogBrandDto> CheckBrandExist(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Delete(CatalogBrandDto brand)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Update(CatalogBrandDto brand)
        {
            throw new NotImplementedException();
        }
    }
}
