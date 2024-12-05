using Entities.Models;

namespace Entities.DTO
{
    public class ProductPagedResponse
    {
        public List<ProductDTO> Items { get; set; }
        public PageMetadata Metadata { get; set; }
    }
}
