using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Infrastructure.Services;

namespace Catalog.Tests.Services;

public class CatalogSubTypeServiceTest
{
    [Fact]
    public async Task AddSubTypeAsync_ReturnsIntSuccessfully()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogSubTypeRepositoryMock = new Mock<ICatalogSubTypeRepository>();
        var subTypeName = "Test subtype";
        var typeId = 1;
        var id = 1;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogSubTypeRepositoryMock.Setup(h => h.Add(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(id);
        var catalogSubTypeService = new CatalogSubTypeService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogSubTypeRepositoryMock.Object);

        var result = await catalogSubTypeService.Add(subTypeName, typeId);
        Assert.NotNull(result);
        Assert.Equal(id, result!.Value);
        catalogSubTypeRepositoryMock.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<int>()), Times.Once());
    }

    [Fact]
    public async Task UpdateSubTypeAsync_ReturnsIntSuccessfully()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogSubTypeRepositoryMock = new Mock<ICatalogSubTypeRepository>();
        var subTypeName = "Test SubType";
        var typeId = 1;
        var subTypeId = 1;
        var id = 1;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogSubTypeRepositoryMock.Setup(h => h.Update(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(id);
        var catalogSubTypeService = new CatalogSubTypeService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogSubTypeRepositoryMock.Object);

        var result = await catalogSubTypeService.Update(subTypeId, subTypeName, typeId);
        Assert.NotNull(result);
        Assert.Equal(id, result!.Value);
        catalogSubTypeRepositoryMock.Verify(
            x => x.Update(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<int>()),
            Times.Once());
    }

    [Fact]
    public async Task DeleteSubTypeAsync_ReturnsIntSuccessfully()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogSubTypeRepositoryMock = new Mock<ICatalogSubTypeRepository>();
        var id = 1;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogSubTypeRepositoryMock.Setup(h => h.Delete(It.IsAny<int>())).ReturnsAsync(id);
        var catalogSubTypeService = new CatalogSubTypeService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogSubTypeRepositoryMock.Object);

        var result = await catalogSubTypeService.Delete(id);
        Assert.NotNull(result);
        Assert.Equal(id, result!.Value);
        catalogSubTypeRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
    }

    [Fact]
    public async Task AddSubTypeAsync_ReturnsIdFailed()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogSubTypeRepositoryMock = new Mock<ICatalogSubTypeRepository>();
        var subTypeName = "Test SubType";
        var typeId = 1;
        int? id = null;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogSubTypeRepositoryMock.Setup(h => h.Add(It.IsAny<string>(), It.IsAny<int>())).ReturnsAsync(id);
        var catalogSubTypeService = new CatalogSubTypeService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogSubTypeRepositoryMock.Object);

        var result = await catalogSubTypeService.Add(subTypeName, typeId);
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateSubTypeAsync_ReturnsIntFailed()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogSubTypeRepositoryMock = new Mock<ICatalogSubTypeRepository>();
        var subTypeName = "Test SubType";
        var subtypeId = 1;
        var typeId = 1;
        int? id = null;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogSubTypeRepositoryMock.Setup(h => h.Update(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<int>()))
            .ReturnsAsync(id);
        var catalogSubTypeService = new CatalogSubTypeService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogSubTypeRepositoryMock.Object);

        var result = await catalogSubTypeService.Update(subtypeId, subTypeName, typeId);
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteSubTypeAsync_ReturnsIntFailed()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogSubTypeRepositoryMock = new Mock<ICatalogSubTypeRepository>();
        int? id = null;
        var requestId = 1;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogSubTypeRepositoryMock.Setup(
            h => h.Delete(
                It.IsAny<int>())).
            ReturnsAsync(id);
        var catalogSubTypeService = new CatalogSubTypeService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogSubTypeRepositoryMock.Object);

        var result = await catalogSubTypeService.Delete(requestId);
        result.Should().BeNull();
    }
}