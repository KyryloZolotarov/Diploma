using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Basket.Host.Models;
using Basket.Host.Services.Interfaces;
using Basket.Host.Services;

namespace Basket.UnitTests.Services
{
    public class BasketServiceTest
    {
        [Fact]
        public async Task Add_ValidBasketIdAndNewItem_Successfully()
        {
            // Arrange
            var basketId = "testBasketId";
            var itemId = 1;

            var existingItem = new BasketItem() { Id = itemId, Count = 2 };
            var basketWithItem = new BasketItemsDb() { Items = new List<BasketItem>() { existingItem } };

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(basketWithItem);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            // Act
            await basketService.Add(basketId, itemId);

            // Assert
            cacheServiceMock.Verify(x => x.AddOrUpdateAsync(basketId, It.Is<BasketItemsDb>(basket => basket.Items.Single().Count == 3)), Times.Once);
        }

        [Fact]
        public async Task Add_Should_ThrowArgumentNullException_Failed()
        {
            // Arrange
            string basketId = "te";
            int itemId = 1;

            var cacheServiceMock = new Mock<ICacheService>();
            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => basketService.Add(basketId, itemId));
        }

        [Fact]
        public async Task AddItems_ValidBasketIdAndNewItem_Successfully()
        {
            var basketId = "testBasketId";
            var item = new BasketItem() { Id = 1, Count = 2 };
            var result = new BasketItemsDb();

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(result); // Simulate an empty basket

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            // Act
            await basketService.AddItems(basketId, item);

            // Assert
            cacheServiceMock.Verify(x => x.AddOrUpdateAsync(basketId, It.IsAny<BasketItemsDb>()), Times.Once);
        }

        [Fact]
        public async Task AddItems_ThrowArgumentNullException_Failed()
        {
            string basketId = string.Empty;
            var item = new BasketItem() { Id = 1, Count = 2 };

            var cacheServiceMock = new Mock<ICacheService>();
            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            // Act & Assert
            await Assert.ThrowsAsync<ArgumentNullException>(() => basketService.AddItems(basketId, item));
        }

        [Fact]
        public async Task ChangeItemsCount_Should_UpdateItemCount_When_ItemExistsInBasket()
        {
            // Arrange
            var basketId = "testBasketId";
            var existingItem = new BasketItem() { Id = 1, Count = 2 };
            var basketWithItem = new BasketItemsDb() { Items = new List<BasketItem>() { existingItem } };
            var updatedItem = new BasketItem() { Id = 1, Count = 3 };

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(basketWithItem);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            // Act
            var updatedBasket = await basketService.ChangeItemsCount(basketId, updatedItem);

            // Assert
            updatedBasket.Should().NotBeNull();
            updatedBasket.Items.Should().NotBeNull();
            updatedBasket.Items.Should().ContainSingle(item => item.Id == updatedItem.Id && item.Count == updatedItem.Count);
        }

        [Fact]
        public async Task ChangeItemsCount_Should_ReturnEmptyBasket_When_BasketDoesNotExist()
        {
            // Arrange
            var basketId = "nonExistentBasketId";
            var item = new BasketItem() { Id = 1, Count = 2 };
            var result = new BasketItemsDb();

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(result);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            // Act
            var updatedBasket = await basketService.ChangeItemsCount(basketId, item);

            // Assert
            updatedBasket.Should().NotBeNull();
            updatedBasket.Items.Should().BeNull();
        }

        [Fact]
        public async Task Get_Should_ReturnBasket_When_BasketExists()
        {
            // Arrange
            var basketId = "testBasketId";
            var expectedBasket = new BasketItemsDb() { /* Initialize with some data */ };

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(expectedBasket);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            // Act
            var result = await basketService.Get(basketId);

            // Assert
            result.Should().NotBeNull();
            result.Should().Be(expectedBasket);
        }

        [Fact]
        public async Task Get_Should_ReturnNull_When_BasketDoesNotExist()
        {
            // Arrange
            var basketId = "nonExistentBasketId";
            var emptyResult = new BasketItemsDb();

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(emptyResult);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            // Act
            var result = await basketService.Get(basketId);

            // Assert
            result.Should().BeNull();
        }

        [Fact]
        public async Task Delete_Should_DeleteBasket_When_BasketExists()
        {
            // Arrange
            var basketId = "testBasketId";

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.Delete(basketId)).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            // Act
            await basketService.Delete(basketId);

            // Assert
            cacheServiceMock.Verify(x => x.Delete(basketId), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_NotThrowException_When_BasketDoesNotExist()
        {
            // Arrange
            var basketId = "nonExistentBasketId";

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.Delete(basketId)).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            // Act & Assert
            await basketService.Delete(basketId); // Should not throw any exceptions
        }

        [Fact]
        public async Task DeleteItem_Should_DeleteItem_When_ItemExistsInBasket()
        {
            // Arrange
            var basketId = "testBasketId";
            var itemId = 1;
            var existingItem = new BasketItem() { Id = itemId, Count = 2 };
            var basketWithItem = new BasketItemsDb() { Items = new List<BasketItem>() { existingItem } };

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(basketWithItem);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            // Act
            var result = await basketService.DeleteItem(basketId, itemId);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().NotContain(item => item.Id == itemId);
        }

        [Fact]
        public async Task DeleteItem_Should_ReturnEmptyBasket_When_BasketOrItemDoesNotExist()
        {
            // Arrange
            var basketId = "testBasketId";
            var itemId = 1;
            var emptyResult = new BasketItemsDb();

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(emptyResult); // Simulate a non-existent basket

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            // Act
            var result = await basketService.DeleteItem(basketId, itemId);

            // Assert
            result.Should().NotBeNull();
            result.Items.Should().BeNull(); // Basket is empty or item doesn't exist
        }
    }
}
