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
            if (countryDTO.CountryId == null)
            {
                return ServiceResult<bool>.BadRequest("CountryId is required.");
            }

            var existingCountry = _repository.GetById(countryDTO.CountryId!.Value);
            if (existingCountry == null)
            {
                return ServiceResult<bool>.NotFound("Country not found.");
            }

            var country = mapper.Map<Country>(countryDTO);
            country.CountryId = existingCountry.CountryId;
            country.UpdatedAt = DateTime.UtcNow;

            try
            {
                _repository.Update(country);
            }
            catch (Exception ex)
            {
                return ServiceResult<bool>.InternalServerError($"An error occurred while updating the country: {ex.Message}");
            }
            return ServiceResult<bool>.Ok(true);
        }
    }
}
