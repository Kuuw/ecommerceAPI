using System;
using System.Collections.Generic;

namespace DAL.Models;

public partial class ProductImage
{
    public int ProductId { get; set; }

    public string ImagePath { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
