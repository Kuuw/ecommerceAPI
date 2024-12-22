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

        public ServiceResult<CountryDTO> Add(CountryDTO countryDTO)
        {
            countryDTO.CountryId = null;
            var country = mapper.Map<Country>(countryDTO);

            _repository.Insert(country);

            countryDTO.CountryId = country.CountryId;
            return ServiceResult<CountryDTO>.Ok(countryDTO);
        }

        public ServiceResult<bool> Delete(int id)
        {
            var country = _repository.GetById(id);
            if (country != null) 
            { 
                _repository.Delete(country);
                return ServiceResult<bool>.Ok(true);
            }
            return ServiceResult<bool>.NotFound("Country not found.");
        }

        public ServiceResult<List<CountryDTO>> Get()
        {
            var countries = _repository.List();
            var list = mapper.Map<List<CountryDTO>>(countries).ToList();
            return ServiceResult<List<CountryDTO>>.Ok(list);
        }

        public ServiceResult<CountryDTO?> GetById(int id)
        {
            var country = _repository.GetById(id);
            if (country != null)
            {
                return ServiceResult<CountryDTO?>.Ok(mapper.Map<CountryDTO>(country));
            }
            return ServiceResult<CountryDTO?>.NotFound("Country not found.");
        }

        public ServiceResult<bool> Update(CountryDTO countryDTO)
        {
            var country = mapper.Map<Country>(countryDTO);
            country.UpdatedAt = DateTime.UtcNow;
            _repository.Update(country);
            Console.WriteLine($"Country {country.CountryName}, {country.CountryId}, {country.CountryPhoneCode}");
            return ServiceResult<bool>.Ok(true);
        }
    }
}
