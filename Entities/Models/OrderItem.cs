using System.Text.Json.Serialization;

namespace Entities.Models;

public partial class OrderItem
{
    public int OrderId { get; set; }

    public int ProductId { get; set; }

    public int Quantity { get; set; }

    public double UnitPrice { get; set; }

    [JsonIgnore]
    public virtual Order Order { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
