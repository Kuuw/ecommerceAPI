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
        private readonly IProductRepository _productRepository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

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
            Product? product = _productRepository.GetById(id);
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
            if (page < 1) { page = 1; }
            if (pageSize > 30 || pageSize < 1) { pageSize = 10; }
            var items = _productRepository.GetPaged(page, pageSize);
            int totalItems = this.GetTotalCount();
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            List<ProductDTO> itemsDTO = mapper.Map<List<Product>,List<ProductDTO>>(items);

            var response = new ProductPagedResponse();
            var metadata = new PageMetadata();
            metadata.Page = page;
            metadata.PageSize = pageSize;
            metadata.TotalPages = totalPages;

            response.Items = itemsDTO;
            response.Metadata = metadata;

            Console.WriteLine($"Metadata: Page={response.Metadata.Page}, PageSize={response.Metadata.PageSize}, TotalPages={response.Metadata.TotalPages}");

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
            productDTO.ProductId = id;
            mapper.Map(productDTO, product);
            product.UpdatedAt = DateTime.Now;

            _productRepository.Update(product);

            return true;
        }

        public int GetStock(int productId)
        {
            var stock = _productRepository.GetStock(productId);
            return stock.Stock;
        }

        public bool UpdateStock(int productId, int stock)
        {
            var product = _productRepository.GetById(productId);
            if(product == null) { return false; }
            _productRepository.UpdateStock(productId, stock);
            return true;
        }

        private int GetTotalCount()
        {
            var size = _productRepository.List().Count;
            return size;
        }
    }
}