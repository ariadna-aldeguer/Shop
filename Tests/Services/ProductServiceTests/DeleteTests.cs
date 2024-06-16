using Api.Models.Dtos;
using Api.Services;
using Data.Database.Entities;
using Data.Exceptions;
using FluentAssertions;
using Moq;
using Tests.Commom;

namespace Tests.Services.ProductServiceTests
{
    public class DeleteTests : MockBase
    {

        public DeleteTests() : base() { }

        [Test]
        public async Task DeleteAsync_ThrowsProductException_WhenProductDoesNotExist()
        {
            var productService = NewProductService();

            Guid nonExistingId = Guid.NewGuid();
            _mockProductRepository.Setup(repo => repo.GetByIdAsync(nonExistingId))
                                  .ReturnsAsync((Product)null);

            Assert.ThrowsAsync<ProductException>(async () => await productService.DeleteAsync(nonExistingId));
        }

        [Test]
        public async Task DeleteAsync_DeletesProduct_WhenProductExists()
        {
            var productService = NewProductService();
            var expectedProduct = new ProductDto { 
                Id = _id,
                Color = "Red",
                Size = "Medium",
                Price = 10,
                Description = "A medium red product"
            };
            var deletedProduct = await productService.DeleteAsync(expectedProduct.Id);

            deletedProduct.Should().NotBeNull();
            deletedProduct.Should().BeEquivalentTo(expectedProduct, options => options.ComparingByMembers<ProductDto>());

            _mockProductRepository.Verify(repo => repo.DeleteAsync(expectedProduct.Id), Times.Once);
        }

    }
}
