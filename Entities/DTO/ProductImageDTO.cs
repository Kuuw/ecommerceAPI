namespace Entities.DTO
{
    public class ProductImageDTO
    {
        public Guid ProductImageId { get; set; }

        public int ProductId { get; set; }

        public string ImagePath { get; set; } = null!;

        public string ImageExtension { get; set; } = null!;
    }
}