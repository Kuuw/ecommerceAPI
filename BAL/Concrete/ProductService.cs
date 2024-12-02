using AutoMapper;
using BAL.Abstract;
using DAL.Concrete;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace BAL.Concrete
{
    public class ProductService : IProductService
    {
        ProductRepository productRepository = new ProductRepository();
        Mapper mapper = MapperConfig.InitializeAutomapper();
        public Product Add(ProductDTO productDTO)
        {
            Product product = new();
            mapper.Map(productDTO, product);

            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            productRepository.Insert(product);

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

        public ProductPagedResponse GetPaged(int page, int pageSize)
        {
            var items = productRepository.GetPaged(page, pageSize);
            int totalItems = this.GetTotalCount();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);

            var response = new ProductPagedResponse();
            response.Items = items;
            response.Metadata.Page = page;
            response.Metadata.PageSize = pageSize;
            response.Metadata.TotalPages = totalPages;

            return response;
        }

        public ProductDTO? GetById(int id)
        {
            var item = productRepository.GetById(id);
            if (item != null)
            {
                return mapper.Map<ProductDTO>(item);
            }
            return null;
        }

        public bool Update(ProductDTO productDTO, int id)
        {
            Product? product = productRepository.GetById(id);

            if (product == null) { return false; }

            mapper.Map(productDTO, product);
            product.UpdatedAt = DateTime.Now;

            productRepository.Update(product);

            return true;
        }

        private int GetTotalCount()
        {
            var size = productRepository.List().Count;
            return size;
        }
    }
}