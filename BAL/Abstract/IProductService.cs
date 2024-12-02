using Entities.DTO;
using Entities.Models;

namespace BAL.Abstract
{
    public interface IProductService
    {
        public Product Add(ProductDTO productDTO);
        public ProductDTO? GetById(int id);
        public ProductPagedResponse GetPaged(int page, int pageSize);
        public void Delete(int id);
        public bool Update(ProductDTO productDTO, int id);
    }
}
