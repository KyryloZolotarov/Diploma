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
    public class CatalogTypeRepository : ICatalogTypeRepository
    {
        private readonly IHttpClientHelper _httpClient;
        private readonly IOptions<AppSettings> _settings;
        public CatalogTypeRepository(IHttpClientHelper httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public Task<int?> Add(CatalogTypeDto type)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Delete(CatalogTypeDto type)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Update(CatalogTypeDto type)
        {
            throw new NotImplementedException();
        }
    }
}
