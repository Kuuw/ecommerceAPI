using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public ServiceResult<CategoryDTO> Add(CategoryDTO categoryDTO)
        {
            var category = mapper.Map<Category>(categoryDTO);
            _repository.Insert(category);
            categoryDTO.CategoryId = category.CategoryId;
            return ServiceResult<CategoryDTO>.Ok(categoryDTO);
        }

        public ServiceResult<bool> Delete(int id)
        {
            var category = _repository.GetById(id);
            if (category != null)
            {
                _repository.Delete(category);
                return ServiceResult<bool>.Ok(true);
            }
            return ServiceResult<bool>.NotFound("Category not found.");
        }

        public ServiceResult<List<CategoryDTO>> Get()
        {
            var list = _repository.List();
            List<CategoryDTO> listDTO;
            listDTO = mapper.Map<List<CategoryDTO>>(list).ToList();
            return ServiceResult<List<CategoryDTO>>.Ok(listDTO);
        }

        public ServiceResult<CategoryDTO?> GetById(int id)
        {
            var category = _repository.GetById(id);
            if (category != null) { return ServiceResult<CategoryDTO?>.Ok(mapper.Map<CategoryDTO>(category)); }
            else { return ServiceResult<CategoryDTO>.NotFound("Category not found."); }
        }

        public ServiceResult<bool> Update(CategoryDTO categoryDTO)
        {
            if(categoryDTO.CategoryId == null) { return ServiceResult<bool>.BadRequest("CategoryId cannot be null."); }
            var category = _repository.GetById((int)categoryDTO.CategoryId);

            if (category == null) { return ServiceResult<bool>.NotFound("Category not found."); }

            mapper.Map(categoryDTO, category);
            _repository.Update(category);
            return ServiceResult<bool>.Ok(true);
        }
    }
}
