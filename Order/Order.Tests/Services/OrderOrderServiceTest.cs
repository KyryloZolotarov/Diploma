using Infrastructure.Exceptions;
using Infrastructure.Services;
using Infrastructure.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.Extensions.Logging;
using Order.Host.Data;
using Order.Hosts.Repositories.Interfaces;
using Order.Hosts.Services;

namespace Order.Tests.Services;
/*
public class OrderOrderServiceTest
{
    [Fact]
    public async Task Add_Successful()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderOrderRepository = new Mock<IOrderOrderRepository>();
        var returnID = 1;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderOrderRepository
            .Setup(repo => repo.Add(It.IsAny<string>(), It.IsAny<DateTime>()))
            .ReturnsAsync(returnID);

        var orderItemService = new OrderOrderService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderOrderRepository.Object);

        var result = await orderItemService.Add("12423", DateTime.Now);

        Assert.NotNull(result);
        Assert.Equal(returnID, result!.Value);

        mockOrderOrderRepository.Verify(x => x.Add(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once());
    }

    [Fact]
    public async Task Add_Failure()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderOrderRepository = new Mock<IOrderOrderRepository>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderOrderRepository
            .Setup(repo => repo.Add(It.IsAny<string>(), It.IsAny<DateTime>()))
            .ThrowsAsync(new BusinessException("Order adding failed"));

        var orderOrderService = new OrderOrderService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderOrderRepository.Object);

        await Assert.ThrowsAsync<BusinessException>(() => orderOrderService.Add("12423", DateTime.Now));
    }

    [Fact]
    public async Task Update_Successful()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderOrderRepository = new Mock<IOrderOrderRepository>();
        var returnID = 1;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderOrderRepository
            .Setup(repo => repo.Update(It.IsAny<string>(), It.IsAny<DateTime>()))
            .ReturnsAsync(returnID);

        var orderOrderService = new OrderOrderService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderOrderRepository.Object);

        var result = await orderOrderService.Update("12423", DateTime.Now);

        Assert.NotNull(result);
        Assert.Equal(returnID, result!.Value);

        mockOrderOrderRepository.Verify(x => x.Update(It.IsAny<string>(), It.IsAny<DateTime>()), Times.Once());
    }

    [Fact]
    public async Task Update_Failure()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderOrderRepository = new Mock<IOrderOrderRepository>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderOrderRepository
            .Setup(repo => repo.Update(It.IsAny<string>(), It.IsAny<DateTime>()))
            .ThrowsAsync(new BusinessException("Order updating failed"));

        var orderOrderService = new OrderOrderService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderOrderRepository.Object);

        await Assert.ThrowsAsync<BusinessException>(() => orderOrderService.Update("12423", DateTime.Now));
    }

    [Fact]
    public async Task Delete_Successful()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderOrderRepository = new Mock<IOrderOrderRepository>();
        var id = 1;

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderOrderRepository
            .Setup(repo => repo.Delete(It.IsAny<int>()))
            .ReturnsAsync(id);

        var orderOrderService = new OrderOrderService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderOrderRepository.Object);

        var result = await orderOrderService.Delete(id);
        Assert.NotNull(result);
        Assert.Equal(id, result!.Value);
        mockOrderOrderRepository.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
    }

    [Fact]
    public async Task Delete_Failure()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderOrderRepository = new Mock<IOrderOrderRepository>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderOrderRepository
            .Setup(repo => repo.Delete(It.IsAny<int>()))
            .ThrowsAsync(new BusinessException("Item deleting failed"));

        var orderOrderService = new OrderOrderService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderOrderRepository.Object);

        await Assert.ThrowsAsync<BusinessException>(() => orderOrderService.Delete(1));
    }
}*/