using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Infrastructure.Services;

namespace Catalog.Tests.Services;

public class CatalogTypeServiceTest
{
    [Fact]
    public async Task AddTypeAsync_ReturnsCatalogTypeIdSuccessfully()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogTypeRepositoryMock = new Mock<ICatalogTypeRepository>();
        var typeName = "Test type";
        var id = 1;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogTypeRepositoryMock.Setup(h => h.Add(It.IsAny<string>())).ReturnsAsync(id);
        var catalogTypeService = new CatalogTypeService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogTypeRepositoryMock.Object);

        var result = await catalogTypeService.Add(typeName);
        Assert.NotNull(result);
        Assert.Equal(id, result!.Value);
        catalogTypeRepositoryMock.Verify(x => x.Add(It.IsAny<string>()), Times.Once());
    }

    [Fact]
    public async Task UpdateTypeAsync_ReturnsCatalogTypeIdSuccessfully()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogTypeRepositoryMock = new Mock<ICatalogTypeRepository>();
        var typeName = "Test type";
        var id = 1;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogTypeRepositoryMock.Setup(h => h.Update(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(id);
        var catalogTypeService = new CatalogTypeService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogTypeRepositoryMock.Object);

        var result = await catalogTypeService.Update(id, typeName);
        Assert.NotNull(result);
        Assert.Equal(id, result!.Value);
        catalogTypeRepositoryMock.Verify(x => x.Update(It.IsAny<int>(), It.IsAny<string>()), Times.Once());
    }

    [Fact]
    public async Task DeleteTypeAsync_ReturnsCatalogTypeIdSuccessfully()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogTypeRepositoryMock = new Mock<ICatalogTypeRepository>();
        var id = 1;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogTypeRepositoryMock.Setup(h => h.Delete(It.IsAny<int>())).ReturnsAsync(id);
        var catalogTypeService = new CatalogTypeService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogTypeRepositoryMock.Object);

        var result = await catalogTypeService.Delete(id);
        Assert.NotNull(result);
        Assert.Equal(id, result!.Value);
        catalogTypeRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
    }

    [Fact]
    public async Task AddBrandAsync_ReturnsCatalogTypeIdFailed()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogTypeRepositoryMock = new Mock<ICatalogTypeRepository>();
        var typeName = "Test type";
        int? id = null;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogTypeRepositoryMock.Setup(h => h.Add(It.IsAny<string>())).ReturnsAsync(id);
        var catalogTypeService = new CatalogTypeService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogTypeRepositoryMock.Object);

        var result = await catalogTypeService.Add(typeName);
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateTypedAsync_ReturnsCatalogTypeIdFailed()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogTypeRepositoryMock = new Mock<ICatalogTypeRepository>();
        var typeName = "Test type";
        var typeId = 1;
        int? id = null;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogTypeRepositoryMock.Setup(h => h.Update(It.IsAny<int>(), It.IsAny<string>())).ReturnsAsync(id);
        var catalogTypeService = new CatalogTypeService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogTypeRepositoryMock.Object);

        var result = await catalogTypeService.Update(typeId, typeName);
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteTypeAsync_ReturnsCatalogTypeIdFailed()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogTypeRepositoryMock = new Mock<ICatalogTypeRepository>();
        int? id = null;
        var requestId = 1;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogTypeRepositoryMock.Setup(h => h.Delete(It.IsAny<int>())).ReturnsAsync(id);
        var catalogTypeService = new CatalogTypeService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogTypeRepositoryMock.Object);

        var result = await catalogTypeService.Delete(requestId);
        result.Should().BeNull();
    }
}