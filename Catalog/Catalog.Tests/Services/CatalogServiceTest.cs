using AutoMapper;
using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Responses;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Infrastructure.Services;
using Catalog.Host.Models.Enums;
using Catalog.Host.Models.Requests;
using Catalog.Host.Models.Requests.UpdateRequsts;

namespace Catalog.Tests.Services
{
    public class CatalogServiceTest
    {
        [Fact]
        public async Task GetCatalogItemsAsync_ReturnsCatalogItemSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var pageSize = 10;
            var pageIndex = 0;
            var filter = new Dictionary<CatalogFilter, int>();

            var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogItem>()
            {
                Data = new List<CatalogItem>()
            {
                new CatalogItem()
                {
                    Name = "TestName",
                },
            },
                TotalCount = 1,
            };

            var catalogItemSuccess = new CatalogItem()
            {
                Name = "TestName",
            };

            var catalogItemDtoSuccess = new CatalogItemDto()
            {
                Name = "TestName",
            };
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetByPageAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>()))
                .ReturnsAsync(pagingPaginatedItemsSuccess);
            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);

            var result = await catalogService.GetCatalogItemsAsync(pageSize, pageIndex, filter);
            Assert.NotNull(result);
            if (result == null)
            {
                return;
            }

            Assert.Equal(pageSize, result.PageSize);
            catalogItemRepositoryMock.Verify(x => x.GetByPageAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>(), It.IsAny<int?>()), Times.Once());
        }

        [Fact]
        public async Task GetCatalogItemByIdAsync_ReturnsCatalogItemSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var id = 1;

            var catalogItemSuccess = new CatalogItem()
            {
                Name = "TestName",
                Id = id,
            };

            var catalogItemDtoSuccess = new CatalogItemDto()
            {
                Name = "TestName",
                Id = id,
            };
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(catalogItemSuccess);
            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);

            var result = await catalogService.GetCatalogItemByIdAsync(id);
            Assert.NotNull(result);
            Assert.Equal(catalogItemSuccess.Id, result.Id);
            catalogItemRepositoryMock.Verify(x => x.GetByIdAsync(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task GetCatalogTypesAsync_ReturnsCatalogTypeSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();

            var typeDtoSuccess = new CatalogTypeDto() { Id = 1, Type = "Type" };
            var typeSuccess = new CatalogType() { Id = 1, Type = "Type" };

            var typesSuccess = new List<CatalogType>()
            {
                new CatalogType()
                {
                    Id = 1,
                    Type = "Type",
                }
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(typeSuccess)))).Returns(typeDtoSuccess);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetTypesAsync()).ReturnsAsync(typesSuccess);
            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);

            var result = await catalogService.GetCatalogTypesAsync();
            Assert.NotNull(result);
            catalogItemRepositoryMock.Verify(x => x.GetTypesAsync(), Times.Once());
        }

        [Fact]
        public async Task GetCatalogBrandsAsync_ReturnsCatalogBrandSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();

            var brandDtoSuccess = new CatalogBrandDto() { Id = 1, Brand = "brand" };
            var brandSuccess = new CatalogBrand() { Id = 1, Brand = "Type" };

            var brandsSuccess = new List<CatalogBrand>()
            {
                new CatalogBrand()
                {
                    Id = 1,
                    Brand = "Type",
                }
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrand>(i => i.Equals(brandSuccess)))).Returns(brandDtoSuccess);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetBrandsAsync()).ReturnsAsync(brandsSuccess);
            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);

            var result = await catalogService.GetCatalogBrandsAsync();
            Assert.NotNull(result);
            catalogItemRepositoryMock.Verify(x => x.GetBrandsAsync(), Times.Once());
        }

        [Fact]
        public async Task GetCatalogSubTypes_ReturnsCatalogSubTypeSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();

            var subTypeDtoSuccess = new CatalogSubTypeDto() { Id = 1, SubType = "Sub Type" };
            var subTypeSuccess = new CatalogSubType() { Id = 1, SubType = "Sub Type" };

            var subTypesSuccess = new List<CatalogSubType>()
            {
                new CatalogSubType()
                {
                    Id = 1,
                    SubType = "Sub Type",
                }
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(s => s.Map<CatalogSubTypeDto>(
            It.Is<CatalogSubType>(i => i.Equals(subTypeSuccess)))).Returns(subTypeDtoSuccess);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetSubTypesAsync(0)).ReturnsAsync(subTypesSuccess);
            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);

            var result = await catalogService.GetCatalogSubTypesAsync(0);
            Assert.NotNull(result);
            catalogItemRepositoryMock.Verify(x => x.GetSubTypesAsync(0), Times.Once());
        }

        [Fact]
        public async Task GetCatalogModels_ReturnsCatalogModelsSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();

            var modelDtoSuccess = new CatalogModelDto() { Id = 1, Model = "Model" };
            var modelSuccess = new CatalogModel() { Id = 1, Model = "Model" };

            var modelsSuccess = new List<CatalogModel>()
            {
                new CatalogModel()
                {
                    Id = 1,
                    Model = "Model",
                }
            };

            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(s => s.Map<CatalogModelDto>(
            It.Is<CatalogModel>(i => i.Equals(modelSuccess)))).Returns(modelDtoSuccess);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetModelsAsync(0)).ReturnsAsync(modelsSuccess);
            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);

            var result = await catalogService.GetCatalogModelsAsync(0);
            Assert.NotNull(result);
            catalogItemRepositoryMock.Verify(x => x.GetModelsAsync(0), Times.Once());
        }

        [Fact]
        public async Task GetCatalogItemsAsync_ReturnsCatalogItemFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var pageSize = 1000;
            var pageIndex = 10000;
            var filter = new Dictionary<CatalogFilter, int>();
            var mapperMock = new Mock<IMapper>();
            var catalogItemFailed = new CatalogItem();
            var catalogItemDtoFailed = new CatalogItemDto();
            mapperMock.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemFailed)))).Returns(catalogItemDtoFailed);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetByPageAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);
            var result = await catalogService.GetCatalogItemsAsync(pageSize, pageIndex, filter);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCatalogItemByIdAsync_ReturnsCatalogItemFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();

            var id = 1000;
            var mapperMock = new Mock<IMapper>();
            var catalogItemFailed = new CatalogItem();
            var catalogItemDtoFailed = new CatalogItemDto();
            mapperMock.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemFailed)))).Returns(catalogItemDtoFailed);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetByIdAsync(It.IsAny<int>())).Returns((Func<CatalogItemDto>)null!);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);
            var result = await catalogService.GetCatalogItemByIdAsync(id);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCatalogModelsAsync_ReturnsCatalogModelFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var mapperMock = new Mock<IMapper>();
            var catalogModelFailed = new CatalogModel();
            var catalogModelDtoFailed = new CatalogModelDto();
            mapperMock.Setup(s => s.Map<CatalogModelDto>(
            It.Is<CatalogModel>(i => i.Equals(catalogModelFailed)))).Returns(catalogModelDtoFailed);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetModelsAsync(0)).Returns((Func<IEnumerable<CatalogSubTypeDto>>)null!);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);
            var result = await catalogService.GetCatalogModelsAsync(0);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCatalogSubTypesAsync_ReturnsCatalogSubTypeFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var mapperMock = new Mock<IMapper>();
            var catalogSubTypeFailed = new CatalogSubType();
            var catalogSubTypeDtoFailed = new CatalogSubTypeDto();
            mapperMock.Setup(s => s.Map<CatalogSubTypeDto>(
            It.Is<CatalogSubType>(i => i.Equals(catalogSubTypeFailed)))).Returns(catalogSubTypeDtoFailed);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetSubTypesAsync(0)).Returns((Func<IEnumerable<CatalogSubTypeDto>>)null!);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);
            var result = await catalogService.GetCatalogSubTypesAsync(0);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCatalogTypesAsync_ReturnsCatalogTypeFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var mapperMock = new Mock<IMapper>();
            var catalogTypeFailed = new CatalogType();
            var catalogTypeDtoFailed = new CatalogTypeDto();
            mapperMock.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(catalogTypeFailed)))).Returns(catalogTypeDtoFailed);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetTypesAsync()).Returns((Func<IEnumerable<CatalogTypeDto>>)null!);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);
            var result = await catalogService.GetCatalogTypesAsync();
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCatalogBrandsAsync_ReturnsCatalogBrandFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var mapperMock = new Mock<IMapper>();
            var catalogBrandFailed = new CatalogBrand();
            var catalogBrandDtoFailed = new CatalogBrandDto();
            mapperMock.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrand>(i => i.Equals(catalogBrandFailed)))).Returns(catalogBrandDtoFailed);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetBrandsAsync()).Returns((Func<IEnumerable<CatalogBrandDto>>)null!);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);
            var result = await catalogService.GetCatalogBrandsAsync();
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetListCatalogItemsAsync_Successfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var mapperMock = new Mock<IMapper>();

            var basketItemsMock = new List<BasketItemDto>() { new BasketItemDto() { Id = 1, Count = 1 } };

            var itemsForBasket = new ItemsForBasketRequest()
                { Items = new List<BasketItemDto>() { new BasketItemDto() { Id = 1, Count = 1 } } };

            var catalogItemSuccess = new CatalogItem()
            {
                Name = "TestName",
                Id = 1,
            };

            var catalogItemDtoSuccess = new CatalogItemDto()
            {
                Name = "TestName",
                Id = 1,
            };

            var listItemsToFront = new BasketItems<CatalogItemDto>() { Items = new List<CatalogItemDto>() { catalogItemDtoSuccess } };
            var listItemsFromBase = new BasketItems<CatalogItem>() { Items = new List<CatalogItem>() { catalogItemSuccess } };

            mapperMock.Setup(s => s.Map<CatalogItemDto>(
                It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetItemsListAsync(It.IsAny<List<BasketItemDto>>())).ReturnsAsync(listItemsFromBase);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);

            var result = await catalogService.GetListCatalogItemsAsync(itemsForBasket);
            Assert.NotNull(result);
            catalogItemRepositoryMock.Verify(x => x.GetItemsListAsync(It.IsAny<List<BasketItemDto>>()), Times.Once());
        }

        [Fact]
        public async Task GetListCatalogItemsAsync_Failed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var mapperMock = new Mock<IMapper>();

            var basketItemsMock = new List<BasketItemDto>();

            var itemsForBasket = new ItemsForBasketRequest();

            var catalogItemFailed = new CatalogItem();

            var catalogItemDtoFailed = new CatalogItemDto();

            var listItemsToFront = new BasketItems<CatalogItemDto>();
            var listItemsFromBase = new BasketItems<CatalogItem>();

            mapperMock.Setup(s => s.Map<CatalogItemDto>(
                It.Is<CatalogItem>(i => i.Equals(catalogItemFailed)))).Returns(catalogItemDtoFailed);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetItemsListAsync(basketItemsMock)).ReturnsAsync(listItemsFromBase);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);
            var result = await catalogService.GetListCatalogItemsAsync(itemsForBasket);
            result.Should().BeNull();
        }

        [Fact]
        public async Task ChangeAvailableItems_Successfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var mapperMock = new Mock<IMapper>();

            var resultSucces = true;

            var catalogItemSuccess = new CatalogItem()
            {
                Name = "TestName",
                Id = 1,
            };

            var catalogItemDtoSuccess = new CatalogItemDto()
            {
                Name = "TestName",
                Id = 1,
            };

            var updateSucces = new UpdateAvailableItemsRequest() { ChangeAvailable = 1, Id = 1 };

            mapperMock.Setup(s => s.Map<CatalogItemDto>(
                It.Is<CatalogItem>(i => i.Equals(catalogItemSuccess)))).Returns(catalogItemDtoSuccess);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.ChangeAvailableItems(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(resultSucces);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);

            var result = await catalogService.ChangeAvailableItems(updateSucces);
            Assert.True(result);
            catalogItemRepositoryMock.Verify(x => x.ChangeAvailableItems(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task ChangeAvailableItems_Failed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var mapperMock = new Mock<IMapper>();

            var resultFailed = false;

            var catalogItemFailed = new CatalogItem();
            var catalogItemDtoFailed = new CatalogItemDto();

            var updateFailed = new UpdateAvailableItemsRequest();

            mapperMock.Setup(s => s.Map<CatalogItemDto>(
                It.Is<CatalogItem>(i => i.Equals(catalogItemFailed)))).Returns(catalogItemDtoFailed);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.ChangeAvailableItems(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(resultFailed);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);
            var result = await catalogService.ChangeAvailableItems(updateFailed);
            Assert.False(result);
        }
    }
}
