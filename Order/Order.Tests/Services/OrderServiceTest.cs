using AutoMapper;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Order.Host.Data;
using Order.Hosts.Data.Entities;
using Order.Hosts.Models.BaseResponses;
using Order.Hosts.Models.Dtos;
using Order.Hosts.Models.Requests;
using Order.Hosts.Models.ToFrontResponses;
using Order.Hosts.Repositories.Interfaces;
using Order.Hosts.Services;

namespace Order.Tests.Services;

public class OrderServiceTest
{
    [Fact]
    public async Task AddOrder_Successful()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderOrderRepository = new Mock<IOrderOrderRepository>();

        var userDtoSuccess = new OrderUserDto { Id = "frewfl", Name = "Name" };
        var userSuccess = new OrderUserEntity { Id = "frewfl", Name = "Name" };
        var listItemsSuccess = new ListItemsForFrontRequest
            { Items = new List<OrderItemDto> { new () { Id = 1, Count = 1, Name = "Part" } } };

        var addingSuccess = true;

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(s => s.Map<OrderUserDto>(
            It.Is<OrderUserEntity>(i => i.Equals(userSuccess)))).Returns(userDtoSuccess);

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderOrderRepository.Setup(h => h.AddOrder(It.IsAny<OrderUserDto>(), It.IsAny<ListItemsForFrontRequest>()))
            .ReturnsAsync(addingSuccess);
        var orderService = new OrderService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderOrderRepository.Object,
            mapperMock.Object);

        var result = await orderService.AddOrder(userDtoSuccess, listItemsSuccess);
        Assert.True(result);
        mockOrderOrderRepository.Verify(
            x => x.AddOrder(
                It.IsAny<OrderUserDto>(),
                It.IsAny<ListItemsForFrontRequest>()),
            Times.Once());
    }

    [Fact]
    public async Task AddOrder_Failed()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderOrderRepository = new Mock<IOrderOrderRepository>();

        var userDtoFailed = new OrderUserDto();
        var userFailed = new OrderUserEntity();
        var listItemsFailed = new ListItemsForFrontRequest
            { Items = new List<OrderItemDto> { new () { Id = 1, Count = 1, Name = "Part" } } };

        var addingFailed = false;

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(s => s.Map<OrderUserDto>(
            It.Is<OrderUserEntity>(i => i.Equals(userFailed)))).Returns(userDtoFailed);

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderOrderRepository.Setup(h => h.AddOrder(It.IsAny<OrderUserDto>(), It.IsAny<ListItemsForFrontRequest>()))
            .ReturnsAsync(addingFailed);
        var orderService = new OrderService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderOrderRepository.Object,
            mapperMock.Object);

        var result = await orderService.AddOrder(userDtoFailed, listItemsFailed);
        Assert.False(result);
    }

    [Fact]
    public async Task GetOrder_Successful()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderOrderRepository = new Mock<IOrderOrderRepository>();

        var orderDtoSuccess = new OrderOrderDto { UserId = "fwewd", DateTime = DateTime.Now, Id = 1 };
        var orderEntitySuccess = new OrderOrderEntity { UserId = "fwewd", DateTime = DateTime.Now, Id = 1 };
        var orderSuccess = new OrderOrderForFrontResponse
            { Order = new OrderOrderDto { UserId = "fwewd", DateTime = DateTime.Now, Id = 1 } };
        var orderFromBaseSuccess = new OrderOrderResponse
            { Order = new OrderOrderEntity { UserId = "fwewd", DateTime = DateTime.Now, Id = 1 } };

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(s => s.Map<OrderOrderDto>(
            It.Is<OrderOrderEntity>(i => i.Equals(orderEntitySuccess)))).Returns(orderDtoSuccess);

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderOrderRepository.Setup(h => h.GetOrder(1)).ReturnsAsync(orderFromBaseSuccess);
        var orderService = new OrderService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderOrderRepository.Object,
            mapperMock.Object);

        var result = await orderService.GetOrder(1);
        mockOrderOrderRepository.Verify(x => x.GetOrder(It.IsAny<int>()), Times.Once());
    }

    [Fact]
    public async Task GetOrder_Failed()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderOrderRepository = new Mock<IOrderOrderRepository>();

        var orderDtoSuccess = new OrderOrderDto();
        var orderEntitySuccess = new OrderOrderEntity();
        var orderSuccess = new OrderOrderForFrontResponse();
        var orderFromBaseSuccess = new OrderOrderResponse();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(s => s.Map<OrderOrderDto>(
            It.Is<OrderOrderEntity>(i => i.Equals(orderEntitySuccess)))).Returns(orderDtoSuccess);

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderOrderRepository.Setup(h => h.GetOrder(1)).ReturnsAsync(orderFromBaseSuccess);
        var orderService = new OrderService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderOrderRepository.Object,
            mapperMock.Object);

        var result = await orderService.GetOrder(1);
        result.Should().BeNull();
    }

    [Fact]
    public async Task GetOrderList_Successful()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderOrderRepository = new Mock<IOrderOrderRepository>();

        var orderDtoSuccess = new OrderOrderDto { DateTime = DateTime.Now, Id = 1, UserId = "213214" };
        var orderSuccess = new OrderOrderEntity { DateTime = DateTime.Now, Id = 1, UserId = "213214" };
        var listItemsSuccess = new ListOrderForFrontResponse
        {
            Orders = new List<OrderOrderDto> { orderDtoSuccess }
        };
        var listOrderBaseResponse = new ListOrdersResponse
        {
            Orders = new List<OrderOrderEntity> { orderSuccess }
        };

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(s => s.Map<OrderOrderDto>(
            It.Is<OrderOrderEntity>(i => i.Equals(orderSuccess)))).Returns(orderDtoSuccess);

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderOrderRepository.Setup(h => h.GetOrderList("213214")).ReturnsAsync(listOrderBaseResponse);
        var orderService = new OrderService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderOrderRepository.Object,
            mapperMock.Object);

        var result = await orderService.GetOrderList("213214");
        Assert.NotNull(listItemsSuccess);
        mockOrderOrderRepository.Verify(x => x.GetOrderList(It.IsAny<string>()), Times.Once());
    }

    [Fact]
    public async Task GetOrderList_Failed()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderOrderRepository = new Mock<IOrderOrderRepository>();

        var orderDtoSuccess = new OrderOrderDto();
        var orderSuccess = new OrderOrderEntity();
        var listItemsSuccess = new ListOrderForFrontResponse();
        var listOrderBaseResponse = new ListOrdersResponse();

        var mapperMock = new Mock<IMapper>();
        mapperMock.Setup(s => s.Map<OrderOrderDto>(
            It.Is<OrderOrderEntity>(i => i.Equals(orderSuccess)))).Returns(orderDtoSuccess);

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderOrderRepository.Setup(h => h.GetOrderList("213214")).ReturnsAsync(listOrderBaseResponse);
        var orderService = new OrderService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderOrderRepository.Object,
            mapperMock.Object);

        var result = await orderService.GetOrderList("213214");
        result.Should().BeNull();
    }
}