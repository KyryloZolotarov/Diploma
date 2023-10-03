using Catalog.Front.Helpers.Interfaces;
using Catalog.Front.Models.Dtos;
using Catalog.Front.Repositories.Intefaces;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Repositories
{
    public class CatalogItemRepository : ICatalogItemRepository
    {
        private readonly IHttpClientHelper _httpClient;
        private readonly IOptions<AppSettings> _settings;
        public CatalogItemRepository(IHttpClientHelper httpClient, IOptions<AppSettings> settings)
        {
            _httpClient = httpClient;
            _settings = settings;
        }

        public Task<int?> Add(CatalogItemDto item)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Delete(CatalogItemDto item)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Update(CatalogItemDto item)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<CatalogItemDto>> GetItemsAsync()
        {
            throw new NotImplementedException();
        }
    }
}
