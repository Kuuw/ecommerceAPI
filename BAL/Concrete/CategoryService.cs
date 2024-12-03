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

        public CategoryDTO Add(CategoryDTO categoryDTO)
        {
            var category = mapper.Map<Category>(categoryDTO);
            _repository.Insert(category);
            categoryDTO.CategoryId = category.CategoryId;
            return categoryDTO;
        }

        public void Delete(int id)
        {
            var category = _repository.GetById(id);
            if (category != null)
            {
                _repository.Delete(category);
            }
        }

        public List<CategoryDTO> Get()
        {
            var listDTO = new List<CategoryDTO>();
            var list = _repository.List();
            for (int i = 0; i < list.Count; i++) 
            {
                listDTO.Add(mapper.Map<CategoryDTO>(list[i]));
            }
            return listDTO;
        }

        public CategoryDTO? GetById(int id)
        {
            var category = _repository.GetById(id);
            if (category != null) { return mapper.Map<CategoryDTO>(category); }
            else { return null; }
        }

        public void Update(CategoryDTO categoryDTO)
        {
            if(categoryDTO.CategoryId == null) { return; }
            var category = _repository.GetById((int)categoryDTO.CategoryId);

            if (category == null) { return; }

            mapper.Map(categoryDTO, category);
            _repository.Update(category);
        }
    }
}
