using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.DTO
{
    public class CartItemDTO
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }

        public virtual Product Product { get; set; } = null!;
    }
}
