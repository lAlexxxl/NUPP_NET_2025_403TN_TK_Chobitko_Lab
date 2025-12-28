using Xunit;
using SmartHome.Common;
using System;
using System.Threading.Tasks;
using System.Linq;

namespace SmartHome.Tests
{
    public class CrudTests
    {
        [Fact]
        public async Task CreateAsync_ShouldAddItemToCollection()
        {
            // Arrange (Підготовка)
            var service = new GenericCrudServiceAsync<Light>();
            var newLamp = Light.CreateNew();

            // Act (Дія)
            await service.CreateAsync(newLamp);
            var items = await service.ReadAllAsync();

            // Assert (Перевірка результату)
            Assert.Contains(newLamp, items);
            Assert.Single(items); // Перевіряємо, що в списку рівно 1 елемент
        }

        [Fact]
        public async Task ReadAllAsync_WithPagination_ShouldReturnCorrectAmount()
        {
            // Arrange
            var service = new GenericCrudServiceAsync<Light>();
            for (int i = 0; i < 10; i++) 
            {
                await service.CreateAsync(Light.CreateNew());
            }

            // Act: беремо 1 сторінку по 3 елементи
            var pagedItems = await service.ReadAllAsync(page: 1, amount: 3);

            // Assert
            Assert.Equal(3, pagedItems.Count());
        }

        [Fact]
        public async Task SaveAsync_ShouldReturnTrue()
        {
            // Arrange
            var service = new GenericCrudServiceAsync<Light>();
            await service.CreateAsync(Light.CreateNew());

            // Act
            var result = await service.SaveAsync();

            // Assert
            Assert.True(result);
        }
    }
}