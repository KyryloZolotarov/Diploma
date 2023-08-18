using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Catalog.Tests.Services
{
    public class CatalogModelServiceTest
    {
        [Fact]
        public async Task AddModelAsync_ReturnsIntSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogModelRepositoryMock = new Mock<ICatalogModelRepository>();
            var modelName = "Test model";
            var brandId = 1;
            var id = 1;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogModelRepositoryMock.Setup(h => h.Add(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(id);
            var catalogModelService = new CatalogModelService(dbContextWrapperMock.Object, loggerMock.Object, catalogModelRepositoryMock.Object);

            var result = await catalogModelService.Add(modelName, brandId);
            Assert.NotNull(result);
            Assert.Equal(id, result.Value);
            catalogModelRepositoryMock.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task UpdateModelAsync_ReturnsIntSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogModelRepositoryMock = new Mock<ICatalogModelRepository>();
            var modelName = "Test model";
            var brandId = 1;
            var modelId = 1;
            var id = 1;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogModelRepositoryMock.Setup(h => h.Update(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(id);
            var catalogModelService = new CatalogModelService(dbContextWrapperMock.Object, loggerMock.Object, catalogModelRepositoryMock.Object);

            var result = await catalogModelService.Update(modelId, modelName, brandId);
            Assert.NotNull(result);
            Assert.Equal(id, result.Value);
            catalogModelRepositoryMock.Verify(x => x.Update(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task DeleteModelAsync_ReturnsIntSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogModelRepositoryMock = new Mock<ICatalogModelRepository>();
            var id = 1;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogModelRepositoryMock.Setup(h => h.Delete(It.IsAny<int>())).ReturnsAsync(id);
            var catalogModelService = new CatalogModelService(dbContextWrapperMock.Object, loggerMock.Object, catalogModelRepositoryMock.Object);

            var result = await catalogModelService.Delete(id);
            Assert.NotNull(result);
            Assert.Equal(id, result.Value);
            catalogModelRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task AddModelAsync_ReturnsIdFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogModelRepositoryMock = new Mock<ICatalogModelRepository>();
            var modelName = "Test model";
            var brandId = 1;
            int? id = null;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogModelRepositoryMock.Setup(h => h.Add(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(id);
            var catalogModelService = new CatalogModelService(dbContextWrapperMock.Object, loggerMock.Object, catalogModelRepositoryMock.Object);

            var result = await catalogModelService.Add(modelName, brandId);
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateModelAsync_ReturnsIntFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogModelRepositoryMock = new Mock<ICatalogModelRepository>();
            var modelName = "Test model";
            var brandId = 1;
            var modelId = 1;
            int? id = null;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogModelRepositoryMock.Setup(h => h.Update(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(id);
            var catalogModelService = new CatalogModelService(dbContextWrapperMock.Object, loggerMock.Object, catalogModelRepositoryMock.Object);

            var result = await catalogModelService.Update(modelId, modelName, brandId);
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteModelAsync_ReturnsIntFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogModelRepositoryMock = new Mock<ICatalogModelRepository>();
            int? id = null;
            var requestId = 1;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogModelRepositoryMock.Setup(h => h.Delete(It.IsAny<int>())).ReturnsAsync(id);
            var catalogModelService = new CatalogModelService(dbContextWrapperMock.Object, loggerMock.Object, catalogModelRepositoryMock.Object);

            var result = await catalogModelService.Delete(requestId);
            result.Should().BeNull();
        }
    }
}
