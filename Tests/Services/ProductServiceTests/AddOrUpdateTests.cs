using Api.Models.Commands.Product;
using Api.Models.Dtos;
using Api.Services;
using Data.Database.Entities;
using Data.Exceptions;
using FluentAssertions;
using Moq;
using Tests.Commom;

namespace Tests.Services.ProductServiceTests
{
    public class AddOrUpdateTests : MockBase
    {

        [Test]
        public async Task AddOrUpdateAsync_Create_ShouldCreateNewProduct_WhenProductCommandIsValid()
        {
            var productService = NewProductService();

            var productCommand = new ProductCommand
            {
                Color = "Red",
                Size = "Medium",
                Price = 10,
                Description = "A medium red product"
            };
            var expectedProductDto = new ProductDto
            {
                Color = "Red",
                Size = "Medium",
                Price = 10,
                Description = "A medium red product"
            };

            var result = await productService.AddOrUpdateAsync(productCommand);

            result.Should().NotBeNull();
            result.Should().BeEquivalentTo(expectedProductDto, options => options.ComparingByMembers<ProductDto>());


            _mockProductRepository.Verify(i => i.AddAsync(It.IsAny<Product>()), Times.Once);
            _mockSizeRepository.Verify(i => i.GetByNameAsync(It.IsAny<string>()), Times.Once);
            _mockColorRepository.Verify(i => i.GetByNameAsync(It.IsAny<string>()), Times.Once);

        }

        [Test]
        public async Task AddOrUpdateAsync_Create_ShouldThrowSizeException_WhenSizeDoesNotExist()
        {
            var productService = NewProductService();

            var productCommand = new ProductCommand { 
                Size = "Not exists",
                Color = "Red",
                Price = 10,
                Description = "A medium red product"
            };
            _mockSizeRepository.Setup(repo => repo.GetByNameAsync(It.IsAny<string>()))
                              .ReturnsAsync((Size)null);


            // Act & Assert
            Assert.ThrowsAsync<SizeException>(() => productService.AddOrUpdateAsync(productCommand));
        }

        [Test]
        public async Task AddOrUpdateAsync_Create_ShouldThrowColorException_WhenColorDoesNotExist()
        {
            var productService = NewProductService();

            var productCommand = new ProductCommand
            {
                Size = "Medium",
                Color = "Black",
                Price = 10,
                Description = "A medium red product"
            };
            _mockSizeRepository.Setup(repo => repo.GetByNameAsync(It.IsAny<string>()))
                              .ReturnsAsync((Size)null);


            // Act & Assert
            Assert.ThrowsAsync<SizeException>(() => productService.AddOrUpdateAsync(productCommand));
        }




        [Test]
        public async Task AddOrUpdateAsync_Update_ShouldThrowSizeException_WhenSizeDoesNotExist()
        {
            var productService = NewProductService();

            var productCommand = new ProductCommand
            {
                Size = "Not exists",
                Color = "Red",
                Price = 10,
                Description = "A medium red product"
            };
            _mockSizeRepository.Setup(repo => repo.GetByNameAsync(It.IsAny<string>()))
                              .ReturnsAsync((Size)null);

            // Act & Assert
            Assert.ThrowsAsync<SizeException>(() => productService.AddOrUpdateAsync(productCommand, _id));
        }

        [Test]
        public async Task AddOrUpdateAsync_Update_ShouldThrowColorException_WhenColorDoesNotExist()
        {
            var productService = NewProductService();

            var productCommand = new ProductCommand
            {
                Size = "Medium",
                Color = "Black",
                Price = 10,
                Description = "A medium red product"
            };
            _mockSizeRepository.Setup(repo => repo.GetByNameAsync(It.IsAny<string>()))
                              .ReturnsAsync((Size)null);


            // Act & Assert
            Assert.ThrowsAsync<SizeException>(() => productService.AddOrUpdateAsync(productCommand, _id));
        }

    }
}
