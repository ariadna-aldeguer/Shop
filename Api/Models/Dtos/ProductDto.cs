namespace Api.Models.Dtos
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public decimal Price { get; set; }
    }
}
