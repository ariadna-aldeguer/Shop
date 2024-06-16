using Api.Models;
using Api.Models.Commands.Product;
using Api.Models.Dtos;
using Api.Models.Queries;
using Moq;
using FluentAssertions;
using Tests.Commom;
using Api.Controllers;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace Tests.Api
{
    public class ProductApiTests : MockBase
    {
        public ProductApiTests() : base() { }

        [Test]
        public async Task GetProducts_ShouldBeCorrect_WhenProductQueryIsValid()
        {
            var query = new ProductQuery { };


            var expectedProducts = new PaginatedList<ProductDto>(
               items: new List<ProductDto>
               {
                    new ProductDto { Size = "Medium", Color = "Red", Price = 10, Description = "A medium red product" },
                    new ProductDto { Size ="Large" , Color = "Blue" , Price = 15, Description = "A large blue product" }

               },
               count: 2,
               pageNumber: 1,
               pageSize: 10
          );

            _mockProductService.Setup(service => service.GetFilteredProductsAsync(query))
                              .ReturnsAsync(expectedProducts);
            var controller = new ProductController(_mockProductService.Object);

            // Act
            var result = await controller.GetProducts(query);

            // Assert 
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();

            var returnedProduct = okResult.Value as PaginatedList<ProductDto>;
            returnedProduct.Should().NotBeNull();

            returnedProduct.Should().BeEquivalentTo(expectedProducts, options => options.ComparingByMembers<ProductDto>());
            _mockProductService.Verify(i => i.GetFilteredProductsAsync(query), Times.Once);
        }

        [Test]
        public async Task CreateProduct_ShouldBeCorrect_WhenProductCommandIsValid()
        {
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

            _mockProductService.Setup(service => service.AddOrUpdateAsync(productCommand, null))
                              .ReturnsAsync(expectedProductDto);
            var controller = new ProductController(_mockProductService.Object);

            // Act
            var result = await controller.CreateProduct(productCommand);

            // Assert 
            var okResult = result.Result as OkObjectResult;
            okResult.Should().NotBeNull();

            var returnedProduct = okResult.Value as ProductDto;
            returnedProduct.Should().NotBeNull();

            returnedProduct.Should().BeEquivalentTo(expectedProductDto, options => options.ComparingByMembers<ProductDto>());
            _mockProductService.Verify(i => i.AddOrUpdateAsync(productCommand, null), Times.Once);
        }

        [Test]
        public async Task UpdateProduct_ShouldBeCorrect_WhenProductCommandIsValid()
        {
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

            _mockProductService.Setup(service => service.AddOrUpdateAsync(productCommand, _id))
                              .ReturnsAsync(expectedProductDto);
            var controller = new ProductController(_mockProductService.Object);

            // Act
            var result = await controller.UpdateProduct(_id, productCommand);

            // Assert 
            var okResult = result as OkObjectResult;
            okResult.Should().NotBeNull();

            var returnedProduct = okResult.Value as ProductDto;
            returnedProduct.Should().NotBeNull();

            returnedProduct.Should().BeEquivalentTo(expectedProductDto, options => options.ComparingByMembers<ProductDto>());
            _mockProductService.Verify(i => i.AddOrUpdateAsync(productCommand, _id), Times.Once);
        }

        [Test]
        public async Task DeleteProduct_ShouldBeCorrect_WhenIdIsValid()
        {
            var productId = Guid.NewGuid();
            var productDto = new ProductDto { Id = productId };
            _mockProductService.Setup(service => service.DeleteAsync(productId))
            .ReturnsAsync(productDto);

            var controller = new ProductController(_mockProductService.Object);

            var result = await controller.DeleteProduct(productId);

            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            Assert.AreEqual(productDto, okResult.Value);
        }


        [Test]
        public void ProductCommandShouldRequireMinimumFields()
        {
            var command = new ProductCommand();
            var validationContext = new ValidationContext(command, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(command, validationContext, validationResults, true);

            // Assert
            Assert.IsFalse(isModelValid);
            Assert.IsNotEmpty(validationResults);
        }

        [Test]
        public void ProductQueryShouldntRequireMinimumFields()
        {
            var query = new ProductQuery();
            var validationContext = new ValidationContext(query, serviceProvider: null, items: null);
            var validationResults = new List<ValidationResult>();

            var isModelValid = Validator.TryValidateObject(query, validationContext, validationResults, true);

            // Assert
            Assert.IsTrue(isModelValid);
            Assert.IsEmpty(validationResults);
        }
    }
}
