using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services.Interfaces;

namespace Catalog.Host.Services
{
    public class CatalogModelService : BaseDataService<ApplicationDbContext>, ICatalogModelService
    {
        private readonly ICatalogModelRepository _catalogModelRepository;
        public CatalogModelService(
            IDbContextWrapper<ApplicationDbContext> dbContextWrapper,
            ILogger<BaseDataService<ApplicationDbContext>> logger,
            ICatalogModelRepository catalogModelRepository)
            : base(dbContextWrapper, logger)
        {
            _catalogModelRepository = catalogModelRepository;
        }

        public Task<int?> Add(string modelName, int brandId)
        {
            return ExecuteSafeAsync(() => _catalogModelRepository.Add(modelName, brandId));
        }

        public Task<int?> Update(int id, string modelName, int brandId)
        {
            return ExecuteSafeAsync(() => _catalogModelRepository.Update(id, modelName, brandId));
        }

        public Task<int?> Delete(int id)
        {
            return ExecuteSafeAsync(() => _catalogModelRepository.Delete(id));
        }
    }
}
