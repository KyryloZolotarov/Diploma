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
            var basketId = "testBasketId";
            var itemId = 1;

            var existingItem = new BasketItem() { Id = itemId, Count = 2 };
            var basketWithItem = new BasketItemsDb() { Items = new List<BasketItem>() { existingItem } };

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(basketWithItem);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);
            await basketService.Add(basketId, itemId);
            cacheServiceMock.Verify(x => x.AddOrUpdateAsync(basketId, It.Is<BasketItemsDb>(basket => basket.Items.Single().Count == 3)), Times.Once);
        }

        [Fact]
        public async Task Add_Should_ThrowArgumentNullException_Failed()
        {
            string basketId = "te";
            int itemId = 1;

            var cacheServiceMock = new Mock<ICacheService>();
            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).Throws<ArgumentNullException>();
            await Assert.ThrowsAsync<ArgumentNullException>(() => basketService.Add(basketId, itemId));
        }

        [Fact]
        public async Task AddItems_ValidBasketIdAndNewItem_Successfully()
        {
            var basketId = "testBasketId";
            var item = new BasketItem() { Id = 1, Count = 2 };
            var resultBasket = new BasketItemsDb();

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(resultBasket);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);
            await basketService.AddItems(basketId, item);
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
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).Throws<ArgumentNullException>();
            await Assert.ThrowsAsync<ArgumentNullException>(() => basketService.AddItems(basketId, item));
        }

        [Fact]
        public async Task ChangeItemsCount_Should_UpdateItemCount_When_ItemExistsInBasket()
        {
            var basketId = "testBasketId";
            var existingItem = new BasketItem() { Id = 1, Count = 2 };
            var basketWithItem = new BasketItemsDb() { Items = new List<BasketItem>() { existingItem } };
            var updatedItem = new BasketItem() { Id = 1, Count = 3 };

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(basketWithItem);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);
            var updatedBasket = await basketService.ChangeItemsCount(basketId, updatedItem);
            updatedBasket.Should().NotBeNull();
            updatedBasket.Items.Should().NotBeNull();
            updatedBasket.Items.Should().ContainSingle(item => item.Id == updatedItem.Id && item.Count == updatedItem.Count);
        }

        [Fact]
        public async Task ChangeItemsCount_Should_ReturnEmptyBasket_When_BasketDoesNotExist()
        {
            var basketId = "nonExistentBasketId";
            var item = new BasketItem() { Id = 1, Count = 2 };
            var resultBasket = new BasketItemsDb();

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(resultBasket);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);
            var updatedBasket = await basketService.ChangeItemsCount(basketId, item);
            updatedBasket.Should().NotBeNull();
            updatedBasket.Items.Should().BeNull();
        }

        [Fact]
        public async Task Get_Should_ReturnBasket_When_BasketExists()
        {
            var basketId = "testBasketId";
            var expectedBasket = new BasketItemsDb() { Items = new List<BasketItem>() };

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(expectedBasket);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);
            var result = await basketService.Get(basketId);

            result.Should().NotBeNull();
            result.Should().Be(expectedBasket);
        }

        [Fact]
        public async Task Get_Should_ThrowArgumentNullException_Failed()
        {
            var basketId = "nonExistentBasketId";
            var resultBasket = new BasketItemsDb();

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).Throws<ArgumentNullException>();

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            await Assert.ThrowsAsync<ArgumentNullException>(() => basketService.Get(basketId));
        }

        [Fact]
        public async Task Delete_Should_DeleteBasket_When_BasketExists()
        {
            var basketId = "testBasketId";

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.Delete(basketId)).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            await basketService.Delete(basketId);

            cacheServiceMock.Verify(x => x.Delete(basketId), Times.Once);
        }

        [Fact]
        public async Task Delete_Should_NotThrowException_When_BasketDoesNotExist()
        {
            var basketId = "nonExistentBasketId";

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.Delete(basketId)).Returns(Task.CompletedTask);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            await basketService.Delete(basketId);
        }

        [Fact]
        public async Task DeleteItem_Should_DeleteItem_When_ItemExistsInBasket()
        {
            var basketId = "testBasketId";
            var itemId = 1;
            var existingItem = new BasketItem() { Id = itemId, Count = 2 };
            var basketWithItem = new BasketItemsDb() { Items = new List<BasketItem>() { existingItem } };

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(basketWithItem);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            var result = await basketService.DeleteItem(basketId, itemId);

            result.Should().NotBeNull();
            result.Items.Should().NotContain(item => item.Id == itemId);
        }

        [Fact]
        public async Task DeleteItem_Should_ReturnEmptyBasket_When_BasketOrItemDoesNotExist()
        {
            var basketId = "testBasketId";
            var itemId = 1;
            var resultBasket = new BasketItemsDb();

            var cacheServiceMock = new Mock<ICacheService>();
            cacheServiceMock.Setup(x => x.GetAsync<BasketItemsDb>(basketId)).ReturnsAsync(resultBasket);

            var loggerMock = new Mock<ILogger<BasketService>>();

            var basketService = new BasketService(loggerMock.Object, cacheServiceMock.Object);

            var result = await basketService.DeleteItem(basketId, itemId);

            result.Should().NotBeNull();
            result.Items.Should().BeNull();
        }
    }
}
