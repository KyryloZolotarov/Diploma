using Catalog.Front.Helpers.Interfaces;
using Catalog.Front.Models.Dtos;
using Catalog.Front.Repositories.Intefaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Repositories
{
    public class CatalogSubTypeRepository : ICatalogSubTypeRepository
    {
        private readonly IHttpClientHelper _httpClient;
        private readonly IOptions<AppSettings> _settings;
        public CatalogSubTypeRepository(IHttpClientHelper httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public Task<int?> Add(CatalogSubTypeDto subType)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Delete(CatalogSubTypeDto subType)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Update(CatalogSubTypeDto subType)
        {
            throw new NotImplementedException();
        }
    }
}
