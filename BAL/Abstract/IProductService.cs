using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;

namespace BAL.Abstract
{
    public interface IProductService
    {
        public Product Add(ProductDTO productDTO);
        public ProductDTO? GetById(int id);
        public ProductPagedResponse GetPaged(int page, int pageSize, ProductFilter? productFilter);
        public void Delete(int id);
        public bool Update(ProductDTO productDTO, int id);
        public int GetStock(int productId);
        public bool UpdateStock(int productId, int stock);
        public string UploadImage(int productId, IFormFile file);
        public List<string> GetImages(int productId);
        public void DeleteImage(Guid guid);
    }
}
