using Entities.DTO;

namespace BAL.Abstract
{
    public interface ICategoryService
    {
        public List<CategoryDTO> Get();
        public CategoryDTO? GetById(int id);
        public void Update(CategoryDTO categoryDTO);
        public void Delete(int id);
        public CategoryDTO Add(CategoryDTO categoryDTO);
    }
}
