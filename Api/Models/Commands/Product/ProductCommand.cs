using Data.Common;
using System.ComponentModel.DataAnnotations;

namespace Api.Models.Commands.Product
{
    public class ProductCommand 
    {
        [Required]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        [Required]
        public string Size { get; set; }
        [Required]
        public string Color { get; set; }
    }
}
