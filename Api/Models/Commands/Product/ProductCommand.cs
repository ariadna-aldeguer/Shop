using Data.Common;

namespace Api.Models.Commands.Product
{
    public class ProductCommand 
    {
        public string? Description { get; set; }
        public decimal? Price { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
    }
}
