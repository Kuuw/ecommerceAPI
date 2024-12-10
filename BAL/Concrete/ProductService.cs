﻿using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using DAL.Concrete;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Extensions.Azure;
using System.IO;

namespace BAL.Concrete
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        private readonly IFileRepository _fileRepository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public ProductService(IProductRepository repository, IFileRepository fileRepository)
        {
            _productRepository = repository;
            _fileRepository = fileRepository;
        }

        public Product Add(ProductDTO productDTO)
        {
            Product product = new();
            mapper.Map(productDTO, product);

            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

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

        public ProductPagedResponse GetPaged(int page, int pageSize, ProductFilter? productFilter)
        {
            if (page < 1) { page = 1; }
            if (pageSize > 30 || pageSize < 1) { pageSize = 10; }
            if (productFilter == null) { productFilter = new ProductFilter(); }

            var items = _productRepository.GetPaged(page, pageSize, productFilter);
            int totalItems = _productRepository.GetFilteredCount(productFilter);
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
            product.UpdatedAt = DateTime.UtcNow;

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

        public string UploadImage(int productId, IFormFile file)
        {
            var product = _productRepository.GetById(productId);
            if (product == null) { throw new Exception("Product not found."); }
            var newGuid = Guid.NewGuid();

            var path = _fileRepository.UploadFile($"{newGuid}.jpg", ConvertIFormFileToFileStream(file));
            _productRepository.AddImage(productId, newGuid, path);
            return path;
        }

        public List<string> GetImages(int productId)
        {
            var product = _productRepository.GetById(productId);
            if (product == null) { throw new Exception("Product not found."); }
            return _productRepository.GetImages(productId).Select(x => x.ImagePath).ToList();
        }

        private FileStream ConvertIFormFileToFileStream(IFormFile formFile)
        {
            var tempFilePath = Path.GetTempFileName();
            using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            return new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);
        }
    }
}