using BAL.Abstract;
using DAL.Concrete;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Concrete
{
    public class ProductService: IProductService
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