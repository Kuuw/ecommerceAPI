using Entities.DTO;
using Entities.Models;
using Microsoft.AspNetCore.Http;

namespace BAL.Abstract
{
    public interface IProductService
    {
        public ServiceResult<bool> Add(ProductDTO productDTO);
        public ServiceResult<ProductDTO?> GetById(int id);
        public ServiceResult<ProductPagedResponse> GetPaged(int page, int pageSize, ProductFilter? productFilter);
        public ServiceResult<bool> Delete(int id);
        public ServiceResult<bool> Update(ProductDTO productDTO, int id);
        public ServiceResult<int> GetStock(int productId);
        public ServiceResult<bool> UpdateStock(int productId, int stock);
        public ServiceResult<string> UploadImage(int productId, IFormFile file);
        public ServiceResult<List<string>> GetImages(int productId);
        public ServiceResult<bool> DeleteImage(Guid guid);
    }
}
