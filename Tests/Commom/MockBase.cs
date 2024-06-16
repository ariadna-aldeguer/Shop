using Api.Models.Dtos;
using Api.Models.Profiles;
using Api.Services;
using Api.Services.Interficies;
using AutoMapper;
using Data.Database;
using Data.Database.Entities;
using Data.Interficies;
using Data.Repository.Interfaces;
using Moq;
using MockQueryable.Moq;

namespace Tests.Commom
{
    public class MockBase
    {
        protected readonly IMapper _mapper;
        protected readonly IConfigurationProvider _configuration;
        protected readonly Mock<IProductRepository> _mockProductRepository;
        protected readonly Mock<IColorRepository> _mockColorRepository;
        protected readonly Mock<ISizeRepository> _mockSizeRepository;
        protected readonly Mock<IProductService> _mockProductService;
        protected readonly Mock<IValidation<Product>> _mockProductValidation;
        protected readonly IProductRepository _productRepository;
        protected readonly static Guid _id = new Guid("9f8b9d64-77ef-4df7-b585-1d708c7f4e9f");
        protected readonly Mock<ApplicationDbContext> _context;

        [SetUp]
        public async Task TestSetUp()
        {
            ResetMocks();
        }

        public MockBase()
        {

            _context = new Mock<ApplicationDbContext>();

            _configuration = new MapperConfiguration(cfg =>
                    cfg.AddProfile<ProductProfile>());
            _configuration.AssertConfigurationIsValid();

            _mapper = _configuration.CreateMapper();

            _mockProductRepository = new Mock<IProductRepository>();
            _mockColorRepository = new Mock<IColorRepository>();
            _mockSizeRepository = new Mock<ISizeRepository>();
            _mockProductService = new Mock<IProductService>();

            _mockProductValidation = new Mock<IValidation<Product>>();


            SeedMocks();
        }

        protected void SeedMocks()
        {
            SeedProductRepository();
            SeedColorRepository();
            SeedSizeRepository();
        }

        public void SeedSizeRepository()
        {
            _mockSizeRepository.Setup(x => x.GetByNameAsync(It.IsAny<string>()))
             .ReturnsAsync(new Size { Name = "Medium" });

        }
        public void SeedColorRepository()
        {
            _mockColorRepository.Setup(x => x.GetByNameAsync(It.IsAny<string>()))
               .ReturnsAsync(new Color { Name = "Red" });
        }

        public void SeedProductRepository()
        {
            var products = new List<Product>
            {
                new Product { Id = _id,  Size = new Size { Name = "Medium" }, Color = new Color { Name = "Red" }, Price = 10, Description = "A medium red product" },
                new Product { Size = new Size { Name = "Large" }, Color = new Color { Name = "Blue" }, Price = 15, Description = "A large blue product" }
            };

            var productDto =  new ProductDto { Id = _id, Size = "Medium", Color = "Red", Price = 10, Description = "A medium red product" };

            var mockProducts = products.AsQueryable().BuildMock();

            _mockProductRepository.Setup(x => x.AddAsync(It.IsAny<Product>()))
                .ReturnsAsync(new Product { Size = new Size { Name = "Medium" }, Color = new Color { Name = "Red" }, Price = 10, Description = "A medium red product" });
            _mockProductRepository.Setup(x => x.UpdateAsync(It.IsAny<Product>()))
                .ReturnsAsync(new Product { Id = _id, Size = new Size { Name = "Medium" }, Color = new Color { Name = "Red" }, Price = 10, Description = "A medium red product" });
            _mockProductRepository.Setup(x => x.AsQueryable()).Returns(mockProducts);
            _mockProductRepository.Setup(x => x.GetByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(products.FirstOrDefault());
            _mockProductRepository.Setup(x => x.ExistsByIdAsync(It.IsAny<Guid>()))
                .ReturnsAsync(true);
            _mockProductRepository.Setup(x => x.DeleteAsync(It.IsAny<Guid>()))
                .Returns(Task.CompletedTask);
        }

        public void ResetMocks()
        {
            _mockProductRepository.ResetCalls();
            _mockColorRepository.ResetCalls();
            _mockSizeRepository.ResetCalls();
        }
        public ProductService NewProductService()
        {
            return new ProductService(_mockProductRepository.Object, _mockSizeRepository.Object, _mockColorRepository.Object, _mapper);
        }
    }
}
