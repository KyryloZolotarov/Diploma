using AutoMapper;
using Catalog.Host.Data.Entities;
using Catalog.Host.Data;
using Catalog.Host.Models.Dtos;
using Catalog.Host.Models.Responses;
using Catalog.Host.Repositories.Interfaces;
using Catalog.Host.Services;
using Infrastructure.Services;
using Catalog.Host.Models.Enums;
using System.Collections.Generic;

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

            catalogItemRepositoryMock.Setup(h => h.GetByPageAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), , It.IsAny<int>())).ReturnsAsync(pagingPaginatedItemsSuccess);
            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);

            var result = await catalogService.GetCatalogItemsAsync(pageSize, pageIndex, filter);
            Assert.NotNull(result);
            Assert.Equal(pageSize, result.PageSize);
            catalogItemRepositoryMock.Verify(x => x.GetByPageAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
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
        public async Task GetCatalogItemsByTypeAsync_ReturnsCatalogItemSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var pageSize = 10;
            var pageIndex = 0;
            var typeId = 1;
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

            catalogItemRepositoryMock.Setup(h => h.GetByTypeAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(pagingPaginatedItemsSuccess);
            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);

            var result = await catalogService.GetCatalogItemsByTypeAsync(typeId, pageSize, pageIndex);
            Assert.NotNull(result);
            Assert.Equal(pageSize, result.PageSize);
            catalogItemRepositoryMock.Verify(x => x.GetByTypeAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task GetCatalogItemsByBrandAsync_ReturnsCatalogItemSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var pageSize = 10;
            var pageIndex = 0;
            var typeId = 1;
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

            catalogItemRepositoryMock.Setup(h => h.GetByBrandAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(pagingPaginatedItemsSuccess);
            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);

            var result = await catalogService.GetCatalogItemsByBrandAsync(typeId, pageSize, pageIndex);
            Assert.NotNull(result);
            Assert.Equal(pageSize, result.PageSize);
            catalogItemRepositoryMock.Verify(x => x.GetByBrandAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task GetCatalogTypes_ReturnsCatalogItemSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var pageSize = 10;
            var pageIndex = 0;
            var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogType>()
            {
                Data = new List<CatalogType>()
            {
                new CatalogType()
                {
                    Type = "TestType",
                },
            },
                TotalCount = 1,
            };

            var catalogTypeSuccess = new CatalogType()
            {
                Type = "TestType",
            };

            var catalogTypeDtoSuccess = new CatalogTypeDto()
            {
                Type = "TestType",
            };
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(s => s.Map<CatalogTypeDto>(
            It.Is<CatalogType>(i => i.Equals(catalogTypeSuccess)))).Returns(catalogTypeDtoSuccess);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetTypesAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(pagingPaginatedItemsSuccess);
            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);

            var result = await catalogService.GetCatalogTypesAsync(pageSize, pageIndex);
            Assert.NotNull(result);
            Assert.Equal(pageSize, result.PageSize);
            catalogItemRepositoryMock.Verify(x => x.GetTypesAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task GetCatalogBrands_ReturnsCatalogItemSuccessfully()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var pageSize = 10;
            var pageIndex = 0;
            var pagingPaginatedItemsSuccess = new PaginatedItems<CatalogBrand>()
            {
                Data = new List<CatalogBrand>()
            {
                new CatalogBrand()
                {
                    Brand = "TestBrand",
                },
            },
                TotalCount = 1,
            };

            var catalogBrandSuccess = new CatalogBrand()
            {
                Brand = "TestBrand",
            };

            var catalogBrandDtoSuccess = new CatalogBrandDto()
            {
                Brand = "TestBrand",
            };
            var mapperMock = new Mock<IMapper>();
            mapperMock.Setup(s => s.Map<CatalogBrandDto>(
            It.Is<CatalogBrand>(i => i.Equals(catalogBrandSuccess)))).Returns(catalogBrandDtoSuccess);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetBrandsAsync(It.IsAny<int>(), It.IsAny<int>())).ReturnsAsync(pagingPaginatedItemsSuccess);
            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);

            var result = await catalogService.GetCatalogBrandsAsync(pageSize, pageIndex);
            Assert.NotNull(result);
            Assert.Equal(pageSize, result.PageSize);
            catalogItemRepositoryMock.Verify(x => x.GetBrandsAsync(It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task GetCatalogItemsAsync_ReturnsCatalogItemFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var pageSize = 1000;
            var pageIndex = 10000;
            var mapperMock = new Mock<IMapper>();
            var catalogItemFailed = new CatalogItem();
            var catalogItemDtoFailed = new CatalogItemDto();
            mapperMock.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemFailed)))).Returns(catalogItemDtoFailed);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetByPageAsync(It.IsAny<int>(), It.IsAny<int>())).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);
            var result = await catalogService.GetCatalogItemsAsync(pageSize, pageIndex);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCatalogItemByIdAsync_ReturnsCatalogItemFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var pageSize = 1000;
            var pageIndex = 10000;
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
        public async Task GetCatalogItemsByBrandAsync_ReturnsCatalogItemFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var pageSize = 1000;
            var pageIndex = 10000;
            var id = 1000;
            var mapperMock = new Mock<IMapper>();
            var catalogItemFailed = new CatalogItem();
            var catalogItemDtoFailed = new CatalogItemDto();
            mapperMock.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemFailed)))).Returns(catalogItemDtoFailed);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetByBrandAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);
            var result = await catalogService.GetCatalogItemsByBrandAsync(id, pageSize, pageIndex);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCatalogItemsByTypeAsync_ReturnsCatalogItemFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var pageSize = 1000;
            var pageIndex = 10000;
            var id = 1000;
            var mapperMock = new Mock<IMapper>();
            var catalogItemFailed = new CatalogItem();
            var catalogItemDtoFailed = new CatalogItemDto();
            mapperMock.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemFailed)))).Returns(catalogItemDtoFailed);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetByTypeAsync(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>())).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);
            var result = await catalogService.GetCatalogItemsByTypeAsync(id, pageSize, pageIndex);
            result.Should().BeNull();
        }


        [Fact]
        public async Task GetCatalogTypesAsync_ReturnsCatalogItemFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var pageSize = 1000;
            var pageIndex = 10000;
            var mapperMock = new Mock<IMapper>();
            var catalogItemFailed = new CatalogItem();
            var catalogItemDtoFailed = new CatalogItemDto();
            mapperMock.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemFailed)))).Returns(catalogItemDtoFailed);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetTypesAsync(It.IsAny<int>(), It.IsAny<int>())).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);
            var result = await catalogService.GetCatalogTypesAsync(pageSize, pageIndex);
            result.Should().BeNull();
        }

        [Fact]
        public async Task GetCatalogBrandsAsync_ReturnsCatalogItemFailed()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var catalogItemRepositoryMock = new Mock<ICatalogItemRepository>();
            var pageSize = 1000;
            var pageIndex = 10000;
            var mapperMock = new Mock<IMapper>();
            var catalogItemFailed = new CatalogItem();
            var catalogItemDtoFailed = new CatalogItemDto();
            mapperMock.Setup(s => s.Map<CatalogItemDto>(
            It.Is<CatalogItem>(i => i.Equals(catalogItemFailed)))).Returns(catalogItemDtoFailed);

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            catalogItemRepositoryMock.Setup(h => h.GetBrandsAsync(It.IsAny<int>(), It.IsAny<int>())).Returns((Func<PaginatedItemsResponse<CatalogItemDto>>)null!);

            var catalogService = new CatalogService(dbContextWrapperMock.Object, loggerMock.Object, catalogItemRepositoryMock.Object, mapperMock.Object);
            var result = await catalogService.GetCatalogBrandsAsync(pageSize, pageIndex);
            result.Should().BeNull();
        }
    }
}
