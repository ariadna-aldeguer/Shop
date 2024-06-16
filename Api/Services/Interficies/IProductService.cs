using Api.Models;
using Api.Models.Commands.Product;
using Api.Models.Dtos;
using Api.Models.Queries;
using Data.Database.Entities;

namespace Api.Services.Interficies
{
    public interface IProductService 
    {
        public Task<ProductDto> AddOrUpdateAsync(ProductCommand command, Guid? id = null);
        public Task<PaginatedList<ProductDto>> GetFilteredProductsAsync(ProductQuery productQuery);
        public Task<ProductDto> DeleteAsync(Guid id);
    }
}
