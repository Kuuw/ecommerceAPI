using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class ProductStock
{
    public int ProductId { get; set; }

    public int Stock { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
