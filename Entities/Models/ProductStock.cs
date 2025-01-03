﻿using System.Text.Json.Serialization;

namespace Entities.Models;

public partial class ProductStock
{
    public int ProductId { get; set; }

    public int Stock { get; set; }

    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

    [JsonIgnore]
    public virtual Product Product { get; set; } = null!;
}
