namespace Entities.DTO
{
    public class ProductDTO
    {
        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public double UnitPrice { get; set; }
    }
}