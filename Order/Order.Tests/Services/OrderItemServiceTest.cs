using Infrastructure.Services.Interfaces;
using Infrastructure.Services;
using Microsoft.Extensions.Logging;
using Moq;
using Order.Host.Data;
using Order.Hosts.Repositories.Interfaces;
using Order.Hosts.Services;

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
            mockOrderItemRepository
                .Setup(repo => repo.Add(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(1); // Поддельный успешный результат добавления

            var orderItemService = new OrderItemService(dbContextWrapperMock.Object, loggerMock.Object, mockOrderItemRepository.Object);

            // Act
            var result = await orderItemService.Add(1, "Product", 10.0m, 1, 1, 5, 1);

            // Assert
            result.Should().Be(1); // Ожидаем успешный результат
        }

        [Fact]
        public async Task Add_Failure()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var mockOrderItemRepository = new Mock<IOrderItemRepository>();
            mockOrderItemRepository
                .Setup(repo => repo.Add(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Ошибка при добавлении")); // Поддельное исключение при добавлении

            var orderItemService = new OrderItemService(dbContextWrapperMock.Object, loggerMock.Object, mockOrderItemRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => orderItemService.Add(1, "Product", 10.0m, 1, 1, 5, 1));
        }

        [Fact]
        public async Task Update_Successful()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var mockOrderItemRepository = new Mock<IOrderItemRepository>();
            mockOrderItemRepository
                .Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ReturnsAsync(1); // Поддельный успешный результат добавления

            var orderItemService = new OrderItemService(dbContextWrapperMock.Object, loggerMock.Object, mockOrderItemRepository.Object);

            // Act
            var result = await orderItemService.Update(1, "Product", 10.0m, 1, 1, 5, 1);

            // Assert
            result.Should().Be(1); // Ожидаем успешный результат
        }

        [Fact]
        public async Task Update_Failure()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var mockOrderItemRepository = new Mock<IOrderItemRepository>();
            mockOrderItemRepository
                .Setup(repo => repo.Update(It.IsAny<int>(), It.IsAny<string>(), It.IsAny<decimal>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>(), It.IsAny<int>()))
                .ThrowsAsync(new Exception("Ошибка при добавлении")); // Поддельное исключение при добавлении

            var orderItemService = new OrderItemService(dbContextWrapperMock.Object, loggerMock.Object, mockOrderItemRepository.Object);

            // Act & Assert
            await Assert.ThrowsAsync<Exception>(() => orderItemService.Update(1, "Product", 10.0m, 1, 1, 5, 1));
        }

        [Fact]
        public async Task Delete_Successful()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var mockOrderItemRepository = new Mock<IOrderItemRepository>();
            mockOrderItemRepository
                .Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(1); // Поддельный успешный результат удаления

            var orderItemService = new OrderItemService(dbContextWrapperMock.Object, loggerMock.Object, mockOrderItemRepository.Object);

            // Act
            var result = await orderItemService.Delete(1);

            // Assert
            result.Should().Equals(1); // Ожидаем успешное удаление
        }

        [Fact]
        public async Task Delete_Failure()
        {
            var dbContextWrapperMock = new Mock<IDbContextWrapper<ApplicationDbContext>>();
            var loggerMock = new Mock<ILogger<BaseDataService<ApplicationDbContext>>>();
            var mockOrderItemRepository = new Mock<IOrderItemRepository>();
            mockOrderItemRepository
                .Setup(repo => repo.Delete(It.IsAny<int>()))
                .ReturnsAsync(5); // Поддельный неуспешный результат удаления

            var orderItemService = new OrderItemService(dbContextWrapperMock.Object, loggerMock.Object, mockOrderItemRepository.Object);

            // Act
            var result = await orderItemService.Delete(1);

            // Assert
            result.Should().Equals(5); // Ожидаем неуспешное удаление
        }
    }
}
