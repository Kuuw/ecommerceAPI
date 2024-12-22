using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.Processing;


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

        public ServiceResult<bool> Add(ProductDTO productDTO)
        {
            Product product = new();
            mapper.Map(productDTO, product);

            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

            _productRepository.Insert(product);

            return ServiceResult<bool>.Ok(true);
        }

        public ServiceResult<bool> Delete(int id)
        {
            Product? product = _productRepository.GetById(id);
            if (product != null)
            {
                _productRepository.Delete(product);
                return ServiceResult<bool>.Ok(true);
            }
            else
            {
                return ServiceResult<bool>.NotFound("Product not found.");
            }
        }

        public ServiceResult<ProductPagedResponse> GetPaged(int page, int pageSize, ProductFilter? productFilter)
        {
            if (page < 1) { page = 1; }
            if (pageSize > 30 || pageSize < 1) { pageSize = 10; }
            if (productFilter == null) { productFilter = new ProductFilter(); }
            var response = new ProductPagedResponse();
            var metadata = new PageMetadata();
            var items = _productRepository.GetPaged(page, pageSize, productFilter);
            
            List<ProductDTO> itemsDTO = mapper.Map<List<Product>, List<ProductDTO>>(items);
            response.Items = itemsDTO;

            int totalItems = _productRepository.GetFilteredCount(productFilter);
            int totalPages = (int)Math.Ceiling((double)totalItems / pageSize);
            metadata.Page = page;
            metadata.PageSize = pageSize;
            metadata.TotalPages = totalPages;

            response.Metadata = metadata;

            return ServiceResult<ProductPagedResponse>.Ok(response);
        }

        public ServiceResult<ProductDTO?> GetById(int id)
        {
            var item = _productRepository.GetById(id);
            if (item != null)
            {
                var product = mapper.Map<ProductDTO>(item);
                return ServiceResult<ProductDTO?>.Ok(product);
            }
            return ServiceResult<ProductDTO?>.NotFound("Product not found.");
        }

        public ServiceResult<bool> Update(ProductDTO productDTO, int id)
        {
            Product? product = _productRepository.GetById(id);

            if (product == null) { return ServiceResult<bool>.NotFound("Product not found"); }
            productDTO.ProductId = id;
            mapper.Map(productDTO, product);
            product.UpdatedAt = DateTime.UtcNow;

            _productRepository.Update(product);

            return ServiceResult<bool>.Ok(true);
        }

        public ServiceResult<int> GetStock(int productId)
        {
            var stock = _productRepository.GetStock(productId);
            return ServiceResult<int>.Ok(stock.Stock);
        }

        public ServiceResult<bool> UpdateStock(int productId, int stock)
        {
            var product = _productRepository.GetById(productId);
            if (product == null) { return ServiceResult<bool>.NotFound("Product not found"); }
            _productRepository.UpdateStock(productId, stock);
            return ServiceResult<bool>.Ok(true);
        }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Interoperability", "CA1416:Validate platform compatibility")]
        public ServiceResult<string> UploadImage(int productId, IFormFile file)
        {
            var product = _productRepository.GetById(productId);
            if (product == null) { throw new Exception("Product not found."); }

            var newGuid = Guid.NewGuid();
            ProductImage productImage = new();
            productImage.ProductId = productId;
            productImage.ProductImageId = newGuid;
            productImage.ImageExtension = "jpg";

            using (var readStream = file.OpenReadStream())
            {
                using (var img = Image.Load(readStream))
                {
                    if (!(img.Metadata.DecodedImageFormat is JpegFormat || img.Metadata.DecodedImageFormat is PngFormat))
                        throw new Exception("Uploaded file should be in png or jpeg.");

                    var tempFile = Path.Combine(Path.GetTempPath(), Path.GetRandomFileName());

                    try
                    {
                        img.Mutate(x => x.Resize(new ResizeOptions
                        {
                            Size = new Size(720, 720),
                            Mode = ResizeMode.Pad
                        }));

                        img.Save(tempFile, new JpegEncoder());

                        using (var tempFileStream = new FileStream(tempFile, FileMode.Open))
                        {
                            var path = _fileRepository.UploadFile($"{newGuid}.jpg", tempFileStream);
                            productImage.ImagePath = path;

                            _productRepository.AddImage(productImage);
                            return ServiceResult<string>.Ok(path);
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                        throw;
                    }
                    finally
                    {
                        if (File.Exists(tempFile))
                        {
                            File.Delete(tempFile);
                        }
                    }
                }
            }
        }

        public ServiceResult<List<string>> GetImages(int productId)
        {
            var product = _productRepository.GetById(productId);
            if (product == null) { throw new Exception("Product not found."); }
            var images = _productRepository.GetImages(productId).Select(x => x.ImagePath).ToList();
            return ServiceResult<List<string>>.Ok(images);
        }

        public ServiceResult<bool> DeleteImage(Guid guid)
        {
            _productRepository.DeleteImage(guid);
            var path = String.Concat(guid.ToString(), ".jpg");
            _fileRepository.DeleteFile(path);
            return ServiceResult<bool>.Ok(true);
        }

        private static FileStream ConvertIFormFileToFileStream(IFormFile formFile)
        {
            var tempFilePath = Path.GetRandomFileName();
            using (var fileStream = new FileStream(tempFilePath, FileMode.Create))
            {
                formFile.CopyTo(fileStream);
            }
            return new FileStream(tempFilePath, FileMode.Open, FileAccess.Read);
        }
    }
}