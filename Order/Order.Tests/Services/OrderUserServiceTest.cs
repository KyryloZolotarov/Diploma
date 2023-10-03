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
public class OrderUserServiceTest
{
    [Fact]
    public async Task Add_Successful()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderUserRepository = new Mock<IOrderUserRepository>();
        var returnID = "1233124";

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderUserRepository
            .Setup(repo => repo.Add(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
            .ReturnsAsync(returnID);

        var orderUserService = new OrderUserService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderUserRepository.Object);

        var result = await orderUserService.Add("12423", "12423", "12423", "12423", "12423", "12423");

        Assert.NotNull(result);
        Assert.Equal(returnID, result);

        mockOrderUserRepository.Verify(
            x => x.Add(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once());
    }

    [Fact]
    public async Task Add_Failure()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderUserRepository = new Mock<IOrderUserRepository>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderUserRepository
            .Setup(repo => repo.Add(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
            .ThrowsAsync(new BusinessException("Order adding failed"));

        var orderUserService = new OrderUserService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderUserRepository.Object);

        await Assert.ThrowsAsync<BusinessException>(() =>
            orderUserService.Add("12423", "12423", "12423", "12423", "12423", "12423"));
    }

    [Fact]
    public async Task Update_Successful()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderUserRepository = new Mock<IOrderUserRepository>();
        var returnID = "1233124";

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderUserRepository
            .Setup(repo => repo.Update(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
            .ReturnsAsync(returnID);

        var orderUserService = new OrderUserService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderUserRepository.Object);

        var result = await orderUserService.Update("12423", "12423", "12423", "12423", "12423", "12423");

        Assert.NotNull(result);
        Assert.Equal(returnID, result);

        mockOrderUserRepository.Verify(
            x => x.Update(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()),
            Times.Once());
    }

    [Fact]
    public async Task Update_Failure()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderUserRepository = new Mock<IOrderUserRepository>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderUserRepository
            .Setup(repo => repo.Update(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
            .ThrowsAsync(new BusinessException("Order adding failed"));

        var orderUserService = new OrderUserService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderUserRepository.Object);

        await Assert.ThrowsAsync<BusinessException>(() =>
            orderUserService.Update("12423", "12423", "12423", "12423", "12423", "12423"));
    }

    [Fact]
    public async Task Delete_Successful()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderUserRepository = new Mock<IOrderUserRepository>();
        var id = "12345";

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderUserRepository
            .Setup(repo => repo.Delete(It.IsAny<string>()))
            .ReturnsAsync(id);

        var orderUserService = new OrderUserService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderUserRepository.Object);

        var result = await orderUserService.Delete(id);
        Assert.NotNull(result);
        Assert.Equal(id, result);
        mockOrderUserRepository.Verify(x => x.Delete(It.IsAny<string>()), Times.Once());
    }

    [Fact]
    public async Task Delete_Failure()
    {
        var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
        var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
        var mockOrderUserRepository = new Mock<IOrderUserRepository>();

        var dbContextTransactionMock = new Mock<IDbContextTransaction>();
        dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None))
            .ReturnsAsync(dbContextTransactionMock.Object);

        mockOrderUserRepository
            .Setup(repo => repo.Delete(It.IsAny<string>()))
            .ThrowsAsync(new BusinessException("User deleting failed"));

        var orderUserService = new OrderUserService(
            dbContextWrapperMock.Object,
            loggerMock.Object,
            mockOrderUserRepository.Object);

        await Assert.ThrowsAsync<BusinessException>(() => orderUserService.Delete("2142141"));
    }
}*/