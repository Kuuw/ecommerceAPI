using DAL.Concrete;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Services
{
    public class ProductService
    {
        ProductRepository productRepository = new ProductRepository();
        public Product AddProduct(Product product)
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            productRepository.Insert(product);

            Console.WriteLine(product.ProductId);
            productRepository.AddStockEntry(product.ProductId, 0);

            return product;
        }
    }
}
