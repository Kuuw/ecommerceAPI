using System.Text.Json.Serialization;

namespace Entities.Models;

public partial class ProductImage
{
    public Guid ProductImageId { get; set; }

    public int ProductId { get; set; }

    public string ImagePath { get; set; } = null!;

    public string ImageExtension { get; set; } = null!;

    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;
}
