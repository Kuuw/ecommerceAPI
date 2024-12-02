using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using DAL.Concrete;
using Entities.DTO;
using Entities.Models;
using Microsoft.EntityFrameworkCore.Query;

namespace BAL.Concrete
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        Mapper mapper = MapperConfig.InitializeAutomapper();

        public ProductService(IProductRepository repository)
        {
            _productRepository = repository;
        }

        public Product Add(ProductDTO productDTO)
        {
            Product product = new();
            mapper.Map(productDTO, product);

            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;

            _productRepository.Insert(product);

            return product;
        }

        public void Delete(int id)
        {
            Product product = _productRepository.GetById(id);
            if (product != null)
            {
                _productRepository.Delete(product);
            }
            else
            {
                Console.WriteLine($"Product with id:{id} is not found.");
            }
        }

        public ProductPagedResponse GetPaged(int page, int pageSize)
        {
            var items = _productRepository.GetPaged(page, pageSize);
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
            var item = _productRepository.GetById(id);
            if (item != null)
            {
                return mapper.Map<ProductDTO>(item);
            }
            return null;
        }

        public bool Update(ProductDTO productDTO, int id)
        {
            Product? product = _productRepository.GetById(id);

            if (product == null) { return false; }

            mapper.Map(productDTO, product);
            product.UpdatedAt = DateTime.Now;

            _productRepository.Update(product);

            return true;
        }

        private int GetTotalCount()
        {
            var size = _productRepository.List().Count;
            return size;
        }
    }
}