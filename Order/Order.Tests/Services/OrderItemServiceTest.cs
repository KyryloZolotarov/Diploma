using Infrastructure.Exceptions;
using Infrastructure.Services.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Order.Host.Data;
using Order.Hosts.Repositories.Interfaces;
using Order.Hosts.Services;
using Microsoft.EntityFrameworkCore.Storage;

namespace Order.Tests.Services
{
    public class OrderItemServiceTest
    {
        [Fact]
        public async Task Add_Successful()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var mockOrderItemRepository = new Mock<IOrderItemRepository>();
            var returnID = 1;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            mockOrderItemRepository
                .Setup(repo => repo.Add(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(returnID);

            var orderItemService = new OrderItemService(dbContextWrapperMock.Object, loggerMock.Object, mockOrderItemRepository.Object);

            var result = await orderItemService.Add(1, "Product", 10.0m, 1, 1, 5, 1);

            Assert.NotNull(result);
            Assert.Equal(returnID, result!.Value);

            mockOrderItemRepository.Verify(x => x.Add(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task Add_Failure()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var mockOrderItemRepository = new Mock<IOrderItemRepository>();

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            mockOrderItemRepository
                .Setup(repo => repo.Add(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new BusinessException("Item adding failed"));

            var orderItemService = new OrderItemService(dbContextWrapperMock.Object, loggerMock.Object, mockOrderItemRepository.Object);

            await Assert.ThrowsAsync<BusinessException>(() => orderItemService.Add(1, "Product", 10.0m, 1, 1, 5, 1));
        }

        [Fact]
        public async Task Update_Successful()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var mockOrderItemRepository = new Mock<IOrderItemRepository>();
            var returnID = 1;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            mockOrderItemRepository
                .Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(returnID);

            var orderItemService = new OrderItemService(dbContextWrapperMock.Object, loggerMock.Object, mockOrderItemRepository.Object);

            var result = await orderItemService.Update(1, 1, "Product", 10.0m, 1, 1, 5, 1);

            Assert.NotNull(result);
            Assert.Equal(returnID, result!.Value);

            mockOrderItemRepository.Verify(x => x.Update(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task Update_Failure()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var mockOrderItemRepository = new Mock<IOrderItemRepository>();

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            mockOrderItemRepository
                .Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new BusinessException("Item updating failed"));

            var orderItemService = new OrderItemService(dbContextWrapperMock.Object, loggerMock.Object, mockOrderItemRepository.Object);

            await Assert.ThrowsAsync<BusinessException>(() => orderItemService.Update(1, 1, "Product", 10.0m, 1, 1, 5, 1));
        }

        [Fact]
        public async Task Delete_Successful()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var mockOrderItemRepository = new Mock<IOrderItemRepository>();
            var id = 1;

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            mockOrderItemRepository
                .Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(id);

            var orderItemService = new OrderItemService(dbContextWrapperMock.Object, loggerMock.Object, mockOrderItemRepository.Object);

            var result = await orderItemService.Delete(id);
            Assert.NotNull(result);
            Assert.Equal(id, result!.Value);
            mockOrderItemRepository.Verify(x => x.Delete(It.IsAny<int>()), Times.Once());
        }

        [Fact]
        public async Task Delete_Failure()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var mockOrderItemRepository = new Mock<IOrderItemRepository>();

            var dbContextTransactionMock = new Mock<IDbContextTransaction>();
            dbContextWrapperMock.Setup(s => s.BeginTransactionAsync(CancellationToken.None)).ReturnsAsync(dbContextTransactionMock.Object);

            mockOrderItemRepository
                .Setup(repo => repo.Delete(It.IsAny<int>()))
                .ThrowsAsync(new BusinessException("Item deleting failed"));

            var orderItemService = new OrderItemService(dbContextWrapperMock.Object, loggerMock.Object, mockOrderItemRepository.Object);

            await Assert.ThrowsAsync<BusinessException>(() => orderItemService.Delete(1));
        }
    }
}
