using Entities.DTO;
using Entities.Models;

namespace BAL.Abstract
{
    public interface ICategoryService
    {
        public ServiceResult<List<CategoryDTO>> Get();
        public ServiceResult<CategoryDTO?> GetById(int id);
        public ServiceResult<bool> Update(CategoryDTO categoryDTO);
        public ServiceResult<bool> Delete(int id);
        public ServiceResult<CategoryDTO> Add(CategoryDTO categoryDTO);
    }
}
