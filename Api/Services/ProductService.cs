using Api.Extensions;
using Api.Models;
using Api.Models.Commands.Product;
using Api.Models.Dtos;
using Api.Models.Queries;
using Api.Services.Interficies;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using Azure.Core;
using Data.Database.Entities;
using Data.Exceptions;
using Data.Interficies;
using Data.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Api.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly ISizeRepository _sizeRepository;
        private readonly IColorRepository _colorRepository;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository productRepository,  
                              ISizeRepository sizeRepository,
                              IColorRepository colorRepository,
                              IMapper mapper) 
        {
            _productRepository = productRepository;
            _colorRepository = colorRepository;
            _sizeRepository = sizeRepository;
            _mapper = mapper;

        }

        public async Task<PaginatedList<ProductDto>> GetFilteredProductsAsync(ProductQuery query)
        {
            var products = _productRepository.AsQueryable();

            if (!string.IsNullOrEmpty(query.Size))
            {
                products = products.Where(p => p.Size.Name == query.Size);
            }

            if (!string.IsNullOrEmpty(query.Color))
            {
                products = products.Where(p => p.Color.Name == query.Color);
            }

            if (query.Price != null)
            {
                products = products.Where(p => p.Price == query.Price.Value);
            }

            if (!string.IsNullOrEmpty(query.Description))
            {
                products = products.Where(p => p.Description.Contains(query.Description));
            }

            return await products.ProjectTo<ProductDto>(_mapper.ConfigurationProvider)
                                .PaginatedListAsync(query.PageNumber, query.PageSize);

        }

        public async Task<ProductDto> AddOrUpdatedAsync(ProductCommand productInsert, Guid? id = null)
        {

            Size size = await _sizeRepository.GetByNameAsync(productInsert.Size);
            if (size == null)
            {
                throw new SizeException($"Size {productInsert.Size} not exists.");
            }

            Color color = await _colorRepository.GetByNameAsync(productInsert.Color);
            if (color == null)
            {
                throw new ColorException($"Color {productInsert.Color} not exists.");
            }

            Product newProduct = new Product()
            {
                SizeId = size.Id,
                ColorId = color.Id,
                Description = productInsert.Description,
                Price = productInsert.Price.Value
            };
          
            var productResult = new Product();

            if(id == null)
            {
                productResult = await _productRepository.AddAsync(newProduct);
            }
            else
            {
                newProduct.Id = id.Value;
                productResult = await _productRepository.UpdateAsync(newProduct);
            }

            return _mapper.Map<Product, ProductDto>(productResult);

        }


        public async Task<ProductDto> DeleteAsync(Guid id)
        {
            var product = await _productRepository.GetByIdAsync(id);
            if (product == null)
            {
                throw new ProductException($"Product with id {id} not exists.");
            }
            await _productRepository.DeleteAsync(id);
            return _mapper.Map<Product, ProductDto>(product);
        }
    }
}
