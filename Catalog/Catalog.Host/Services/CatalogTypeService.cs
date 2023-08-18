using Catalog.Host.Data;
using Catalog.Host.Repositories;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogTypeService : BaseDataService<ApplicationDbContext>, ICatalogTypeService
    {
        private readonly ICatalogTypeRepository _catalogTypeRepository;
        public CatalogTypeService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogTypeRepository catalogTypeRepository)
            : base(dbContextWrapper, logger)
        {
            _catalogTypeRepository = catalogTypeRepository;
        }

        public Task<int?> Add(string typeName)
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.Add(typeName));
        }

        public Task<int?> Update(int id, string typeName)
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.Update(id, typeName));
        }

        public Task<int?> Delete(int id)
        {
            return ExecuteSafeAsync(() => _catalogTypeRepository.Delete(id));
        }
    }
}
