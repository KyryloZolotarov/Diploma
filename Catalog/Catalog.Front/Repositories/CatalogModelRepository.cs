using Catalog.Front.Helpers.Interfaces;
using Catalog.Front.Models;
using Catalog.Front.Repositories.Intefaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Repositories
{
    public class CatalogModelRepository : ICatalogModelRepository
    {
        private readonly IHttpClientHelper _httpClient;
        private readonly IOptions<AppSettings> _settings;
        public CatalogModelRepository(IHttpClientHelper httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public Task<int?> Add(CatalogModelDto model)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Delete(CatalogModelDto model)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Update(CatalogModelDto model)
        {
            throw new NotImplementedException();
        }
    }
}
