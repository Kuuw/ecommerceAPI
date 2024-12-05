using Entities.Models;

namespace Entities.DTO
{
    public class ProductDTO
    {
        public int? ProductId { get; set; }

        public string Name { get; set; } = null!;

        public string? Description { get; set; }

        public int CategoryId { get; set; }

        public double UnitPrice { get; set; }

        public ProductStock? ProductStock { get; set; }
    }
}