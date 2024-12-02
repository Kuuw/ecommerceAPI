using Entities.Models;

namespace Entities.DTO
{
    public class ProductPagedResponse
    {
        public List<Product> Items { get; set; }
        public PageMetadata Metadata { get; set; }
    }
}
