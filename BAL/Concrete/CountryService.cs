using AutoMapper;
using BAL.Abstract;
using DAL.Abstract;
using Entities.DTO;
using Entities.Models;

namespace BAL.Concrete
{
    public class CountryService : ICountryService
    {
        private readonly ICountryRepository _repository;
        private readonly Mapper mapper = MapperConfig.InitializeAutomapper();

        public CountryService(ICountryRepository repository)
        {
            _repository = repository;
        }

        public CountryDTO Add(CountryDTO countryDTO)
        {
            countryDTO.CountryId = null;
            var country = mapper.Map<Country>(countryDTO);

            _repository.Insert(country);

            countryDTO.CountryId = country.CountryId;
            return countryDTO;
        }

        public void Delete(int id)
        {
            var country = _repository.GetById(id);
            if (country != null) { _repository.Delete(country); }
        }

        public List<CountryDTO> Get()
        {
            var list = new List<CountryDTO>();
            var countries = _repository.List();
            foreach (var country in countries) 
            {
                list.Add(mapper.Map<CountryDTO>(country));
            }
            return list;
        }

        public CountryDTO? GetById(int id)
        {
            var country = _repository.GetById(id);
            if (country != null)
            {
                return mapper.Map<CountryDTO>(country);
            }
            return null;
        }

        public void Update(CountryDTO countryDTO)
        {
            if (countryDTO.CountryId != null)
            {
                _repository.Update(mapper.Map<Country>(countryDTO));
            }
        }
    }
}
