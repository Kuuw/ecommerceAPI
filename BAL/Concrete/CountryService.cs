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
            var countries = _repository.List();
            var list = mapper.Map<List<CountryDTO>>(countries).ToList();
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
            var country = mapper.Map<Country>(countryDTO);
            country.UpdatedAt = DateTime.UtcNow;
            _repository.Update(country);
            Console.WriteLine($"Country {country.CountryName}, {country.CountryId}, {country.CountryPhoneCode}");
        }
    }
}
