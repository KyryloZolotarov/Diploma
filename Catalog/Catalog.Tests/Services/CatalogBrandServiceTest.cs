using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Infrastructure.Services;

namespace Catalog.Tests.Services
{
    public class CatalogBrandServiceTest
    {
        [Fact]
        public async Task AddBrandAsync_ReturnsCatalogBrandSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogBrandRepositoryMock = new Mock<ICatalogBrandRepository>();
            var brandName = "Test brand";
            var id = 1;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogBrandRepositoryMock.Setup(h => h.Add(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(id);
            var catalogBrandService = new CatalogBrandService(dbContextWrapperMock.Object, loggerMock.Object, catalogBrandRepositoryMock.Object);

            var result = await catalogBrandService.Add(id, brandName);
            Assert.NotNull(result);
            Assert.Equal(id, result.Value);
            catalogBrandRepositoryMock.Verify(x => x.Add(It.IsAny<int>(), It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task UpdateBrandAsync_ReturnsCatalogBrandSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogBrandRepositoryMock = new Mock<ICatalogBrandRepository>();
            var brandName = "Test brand";
            var id = 1;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogBrandRepositoryMock.Setup(h => h.Update(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(id);
            var catalogBrandService = new CatalogBrandService(dbContextWrapperMock.Object, loggerMock.Object, catalogBrandRepositoryMock.Object);

            var result = await catalogBrandService.Update(id, brandName);
            Assert.NotNull(result);
            Assert.Equal(id, result.Value);
            catalogBrandRepositoryMock.Verify(x => x.Update(It.IsAny<int>(), It.IsAny<string>()), Times.Once());
        }

        [Fact]
        public async Task DeleteBrandAsync_ReturnsCatalogBrandSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogBrandRepositoryMock = new Mock<ICatalogBrandRepository>();
            var id = 1;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogBrandRepositoryMock.Setup(h => h.Delete(It.IsAny<int>())).ReturnsAsync(id);
            var catalogBrandService = new CatalogBrandService(dbContextWrapperMock.Object, loggerMock.Object, catalogBrandRepositoryMock.Object);

            var result = await catalogBrandService.Delete(id);
            Assert.NotNull(result);
            Assert.Equal(id, result.Value);
            catalogBrandRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task AddBrandAsync_ReturnsCatalogBrandFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogBrandRepositoryMock = new Mock<ICatalogBrandRepository>();
            var brandName = "Test brand";
            var brandId = 1;
            int? id = null;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogBrandRepositoryMock.Setup(h => h.Add(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(id);
            var catalogBrandService = new CatalogBrandService(dbContextWrapperMock.Object, loggerMock.Object, catalogBrandRepositoryMock.Object);

            var result = await catalogBrandService.Add(brandId, brandName);
            result.Should().BeNull();
        }

        [Fact]
        public async Task UpdateBrandAsync_ReturnsCatalogBrandFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogBrandRepositoryMock = new Mock<ICatalogBrandRepository>();
            var brandName = "Test brand";
            var brandId = 1;
            int? id = null;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogBrandRepositoryMock.Setup(h => h.Update(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(id);
            var catalogBrandService = new CatalogBrandService(dbContextWrapperMock.Object, loggerMock.Object, catalogBrandRepositoryMock.Object);

            var result = await catalogBrandService.Update(brandId, brandName);
            result.Should().BeNull();
        }

        [Fact]
        public async Task DeleteBrandAsync_ReturnsCatalogBrandFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogBrandRepositoryMock = new Mock<ICatalogBrandRepository>();
            int? id = null;
            var requestId = 1;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogBrandRepositoryMock.Setup(h => h.Delete(It.IsAny<int>())).ReturnsAsync(id);
            var catalogBrandService = new CatalogBrandService(dbContextWrapperMock.Object, loggerMock.Object, catalogBrandRepositoryMock.Object);

            var result = await catalogBrandService.Delete(requestId);
            result.Should().BeNull();
        }
    }
}
