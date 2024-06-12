using Data.Database.Entities;
using Data.Exceptions;
using Data.Interficies;
using Data.Repository.Interfaces;

namespace Data.Database.Validations
{
    public class ProductValidation : IValidation<Product>
    {
       
        public ProductValidation()
        {
        }

        public async Task Validate(Product product)
        {
            if (product == null)
            {
                throw new ProductException("There is no product.");
            }

            if (product.SizeId == null)
            {
                throw new ProductException("Size is required.");
            }

            if (product.ColorId == null)
            {
                throw new ProductException("Color is required.");
            }

            if (product.Price <= 0)
            {
                throw new ProductException("Price must be greater than zero.");
            }

            if (string.IsNullOrWhiteSpace(product.Description))
            {
                throw new ProductException("Description is required.");
            }

        }

    }
}
