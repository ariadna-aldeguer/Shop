using Api.Models;
using Api.Models.Dtos;
using Api.Models.Queries;
using Api.Services;
using Moq;
using FluentAssertions;
using Tests.Commom;
using Microsoft.AspNetCore.Mvc;

namespace Tests.Services.ProductServiceTests
{
    public class GetFilteredProductsTests : MockBase
    {


        public GetFilteredProductsTests() : base()
        {
        }

        [Test]
        public async Task GetFilteredProductsAsync_NoFilter_ReturnsExpectedProducts()
        {

            var productService = NewProductService();
            var query = new ProductQuery { };


            var expectedProducts = new PaginatedList<ProductDto>(
                 items: new List<ProductDto>
                 {
                    new ProductDto { Id = _id, Size = "Medium", Color = "Red", Price = 10, Description = "A medium red product" },
                    new ProductDto { Size ="Large" , Color = "Blue" , Price = 15, Description = "A large blue product" }

                 },
                 count: 2,
                 pageNumber: 1,
                 pageSize: 10
            );

            // Act
            var result = await productService.GetFilteredProductsAsync(query);

            // Assert
            result.Should().NotBeNull();
            expectedProducts.Should().BeEquivalentTo(result);
            _mockProductRepository.Verify(i => i.AsQueryable(), Times.Once);
            _mockSizeRepository.Verify(i => i.GetByNameAsync(It.IsAny<string>()), Times.Never);
            _mockColorRepository.Verify(i => i.GetByNameAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task GetFilteredProductsAsync_FiltersBySize_ReturnsExpectedProducts()
        {

            var productService = NewProductService();
            var query = new ProductQuery { Size = "Medium", PageNumber = 1, PageSize = 10 };


            var expectedProducts = new PaginatedList<ProductDto>(
                 items: new List<ProductDto>
                 {
                    new ProductDto { Id = _id,  Size = "Medium", Color = "Red", Price = 10, Description = "A medium red product" }
                 },
                 count: 1,
                 pageNumber: 1,
                 pageSize: 10
            );
            // Act
            var result = await productService.GetFilteredProductsAsync(query);

            // Assert
            result.Should().NotBeNull();
            expectedProducts.Should().BeEquivalentTo(result);
            _mockProductRepository.Verify(i => i.AsQueryable(), Times.Once);
            _mockSizeRepository.Verify(i => i.GetByNameAsync(It.IsAny<string>()), Times.Never);
            _mockColorRepository.Verify(i => i.GetByNameAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task GetFilteredProductsAsync_FiltersByColor_ReturnsExpectedProducts()
        {
            var productService = NewProductService();
            var query = new ProductQuery { Color = "Red", PageNumber = 1, PageSize = 10 };


            var expectedProducts = new PaginatedList<ProductDto>(
                 items: new List<ProductDto>
                 {
                    new ProductDto { Id = _id, Size = "Medium", Color = "Red", Price = 10, Description = "A medium red product" }
                 },
                 count: 1,
                 pageNumber: 1,
                 pageSize: 10
            );

            // Act
            var result = await productService.GetFilteredProductsAsync(query);

            // Assert
            result.Should().NotBeNull();
            expectedProducts.Should().BeEquivalentTo(result);
            _mockProductRepository.Verify(i => i.AsQueryable(), Times.Once);
            _mockColorRepository.Verify(i => i.GetByNameAsync(It.IsAny<string>()), Times.Never);
            _mockSizeRepository.Verify(i => i.GetByNameAsync(It.IsAny<string>()), Times.Never);
        }

        [Test]
        public async Task GetFilteredProductsAsync_FiltersByDescription_ReturnsExpectedProducts()
        {

            var productService = NewProductService();
            var query = new ProductQuery { Description = "Medium", PageNumber = 1, PageSize = 10 };


            var expectedProducts = new PaginatedList<ProductDto>(
                 items: new List<ProductDto>
                 {
                    new ProductDto { Id = _id, Size = "Medium", Color = "Red", Price = 10, Description = "A medium red product" }
                 },
                 count: 1,
                 pageNumber: 1,
                 pageSize: 10
            );

            // Act
            var result = await productService.GetFilteredProductsAsync(query);

            // Assert
            result.Should().NotBeNull();
            expectedProducts.Should().BeEquivalentTo(result);
            _mockProductRepository.Verify(i => i.AsQueryable(), Times.Once);
            _mockColorRepository.Verify(i => i.GetByNameAsync(It.IsAny<string>()), Times.Never);
            _mockSizeRepository.Verify(i => i.GetByNameAsync(It.IsAny<string>()), Times.Never);

        }

        [Test]
        public async Task GetFilteredProductsAsync_FiltersByPrice_ReturnsExpectedProducts()
        {

            var productService = NewProductService();
            var query = new ProductQuery { Price = 10, PageNumber = 1, PageSize = 10 };


            var expectedProducts = new PaginatedList<ProductDto>(
                 items: new List<ProductDto>
                 {
                    new ProductDto { Id = _id, Size = "Medium", Color = "Red", Price = 10, Description = "A medium red product" }
                 },
                 count: 1,
                 pageNumber: 1,
                 pageSize: 10
            );

            // Act
            var result = await productService.GetFilteredProductsAsync(query);

            // Assert
            result.Should().NotBeNull();
            expectedProducts.Should().BeEquivalentTo(result);
            _mockColorRepository.Verify(i => i.GetByNameAsync(It.IsAny<string>()), Times.Never);
            _mockSizeRepository.Verify(i => i.GetByNameAsync(It.IsAny<string>()), Times.Never);
        }
    }
}
