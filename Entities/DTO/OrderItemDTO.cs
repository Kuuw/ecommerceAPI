﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class OrderItemDTO
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public double UnitPrice { get; set; }

        public ProductDTO? Product { get; set; } = null!;
    }
}
