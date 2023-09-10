using Catalog.Host.Data;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Infrastructure.Services;

namespace Catalog.Tests.Services;

public class CatalogItemServiceTest
{
    [Fact]
    public async Task AddItemAsync_ReturnsCatalogItemSuccessfully()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
        var itemName = "Test item";
        var description = "Test descriprion";
        var pictureFileName = "test picture";
        var availiableStock = 3;
        var price = 45;
        var id = 1;
        var subTypeId = 1;
        var modelId = 1;
        var partNumber = "ewdfwefw";

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);
        catalogItemRepositoryMock.Setup(
                h => h.Add(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<decimal>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>()))
            .ReturnsAsync(id);
        var catalogItemService = new CatalogItemService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogItemRepositoryMock.Object);
        var result = await catalogItemService.Add(itemName, description, price, availiableStock, pictureFileName, subTypeId, modelId, partNumber);
        Assert.NotNull(result);
        Assert.Equal(id, result!.Value);
        catalogItemRepositoryMock.Verify(
            x => x.Add(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>()),
            Times.Once());
    }

    [Fact]
    public async Task UpdateItemAsync_ReturnsCatalogItemSuccessfully()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
        var itemName = "Test item";
        var description = "Test descriprion";
        var pictureFileName = "test picture";
        var availiableStock = 3;
        var price = 45;
        var id = 1;
        var subTypeId = 1;
        var modelId = 1;
        var partNumber = "ewdfwefw";

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogItemRepositoryMock.Setup(
            h => h.Update(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>()))
            .ReturnsAsync(id);
        var catalogItemService = new CatalogItemService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogItemRepositoryMock.Object);

        var result = await catalogItemService.Update(id, itemName, description, price, availiableStock, pictureFileName, subTypeId, modelId, partNumber);
        Assert.NotNull(result);
        Assert.Equal(id, result!.Value);
        catalogItemRepositoryMock.Verify(
            x => x.Update(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>()),
            Times.Once());
    }

    [Fact]
    public async Task DeleteItemAsync_ReturnsCatalogItemSuccessfully()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
        var id = 1;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogItemRepositoryMock.Setup(h => h.Delete(It.IsAny<int>())).ReturnsAsync(id);
        var catalogItemService = new CatalogItemService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogItemRepositoryMock.Object);

        var result = await catalogItemService.Delete(id);
        Assert.NotNull(result);
        Assert.Equal(id, result!.Value);
        catalogItemRepositoryMock.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
    }

    [Fact]
    public async Task AddItemAsync_ReturnsCatalogItemFailed()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
        var itemName = "Test item";
        var description = "Test descriprion";
        var pictureFileName = "test picture";
        var availiableStock = 3;
        var price = 45;
        int? id = null;
        var subTypeId = 1;
        var modelId = 1;
        var partNumber = "ewdfwefw";

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogItemRepositoryMock.Setup(
                h => h.Add(
                    It.IsAny<string>(),
                    It.IsAny<string>(),
                    It.IsAny<decimal>(),
                    It.IsAny<int>(),
                    It.IsAny<string>(),
                    It.IsAny<int>(),
                    It.IsAny<int>(),
                    It.IsAny<string>()))
            .ReturnsAsync(id);
        var catalogItemService = new CatalogItemService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogItemRepositoryMock.Object);

        var result = await catalogItemService.Add(itemName, description, price, availiableStock, pictureFileName, subTypeId, modelId, partNumber);
        result.Should().BeNull();
    }

    [Fact]
    public async Task UpdateItemAsync_ReturnsCatalogItemFailed()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
        var itemName = "Test item";
        var description = "Test descriprion";
        var pictureFileName = "test picture";
        var availiableStock = 3;
        var price = 45;
        int? id = null;
        var itemId = 1;

        var subTypeId = 1;
        var modelId = 1;
        var partNumber = "ewdfwefw";

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogItemRepositoryMock.Setup(
            h => h.Update(
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<decimal>(),
                It.IsAny<int>(),
                It.IsAny<string>(),
                It.IsAny<int>(),
                It.IsAny<int>(),
                It.IsAny<string>()))
            .ReturnsAsync(id);
        var catalogItemService = new CatalogItemService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogItemRepositoryMock.Object);

        var result = await catalogItemService.Update(itemId, itemName, description, price, availiableStock, pictureFileName, subTypeId, modelId, partNumber);
        result.Should().BeNull();
    }

    [Fact]
    public async Task DeleteItemAsync_ReturnsCatalogItemFailed()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
        int? id = null;
        var requestId = 1;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        catalogItemRepositoryMock.Setup(h => h.Delete(It.IsAny<int>())).ReturnsAsync(id);
        var catalogItemService = new CatalogItemService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            catalogItemRepositoryMock.Object);

        var result = await catalogItemService.Delete(requestId);
        result.Should().BeNull();
    }
}