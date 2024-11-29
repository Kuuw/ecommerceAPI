using BAL.Abstract;
using DAL.Concrete;
using Entities.Models;

namespace BAL.Concrete
{
    public class ProductService : IProductService
    {
        ProductRepository productRepository = new ProductRepository();
        public Product Add(Product product)
        {
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            productRepository.Insert(product);

            Console.WriteLine(product.ProductId);
            productRepository.AddStockEntry(product.ProductId, 0);

            return product;
        }

        public void Delete(int id)
        {
            Product product = productRepository.GetById(id);
            if (product != null)
            {
                productRepository.Delete(product);
            }
            else
            {
                Console.WriteLine($"Product with id:{id} is not found.");
            }
        }

        public List<Product> GetPaged(int page, int pageSize)
        {
            var items = productRepository.GetPaged(page, pageSize);

            return items;
        }

        public Product? GetById(int id)
        {
            var item = productRepository.GetById(id);
            return item;
        }

        public void Update(Product product)
        {
            productRepository.Update(product);
        }
    }
}