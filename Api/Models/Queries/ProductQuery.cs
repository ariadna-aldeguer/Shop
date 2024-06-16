namespace Api.Models.Queries
{
    public class ProductQuery : GenericQuery
    {
        public string? Description { get; set; }
        public string? Size { get; set; }
        public string? Color { get; set; }
        public decimal? Price { get; set; }
    }
}
