using Entities.DTO;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Abstract
{
    public interface ICountryService
    {
        public ServiceResult<List<CountryDTO>> Get();
        public ServiceResult<CountryDTO?> GetById(int id);
        public ServiceResult<bool> Update(CountryDTO countryDTO);
        public ServiceResult<bool> Delete(int id);
        public ServiceResult<CountryDTO> Add(CountryDTO countryDTO);
    }
}
