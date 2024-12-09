namespace Entities.DTO
{
    public class ProductFilter
    {
        public string? ProductName { get; set; }
        public int? CategoryId { get; set; }
        public double? MinPrice { get; set; }
        public double? MaxPrice { get; set; }

        public ProductFilter()
        {
            CategoryId = null;
            MinPrice = null;
            MaxPrice = null;
        }
    }
}