﻿using Catalog.Front.Repositories.Intefaces;
using Catalog.Front.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalog.Front.Services
{
    public class CatalogSubTypeService : ICatalogSubTypeService
    {
        public readonly ICatalogModelRepository _catalogModelRepository;
        public CatalogSubTypeService(ICatalogModelRepository catalogModelRepository)
        {
            _catalogModelRepository = catalogModelRepository;
        }

        public Task<int?> Add(string subTypeName, int typeId)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Delete(int id)
        {
            throw new NotImplementedException();
        }

        public Task<int?> Update(int id, string subTypeName, int typeId)
        {
            throw new NotImplementedException();
        }
    }
}
