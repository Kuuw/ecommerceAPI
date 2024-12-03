using Entities.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BAL.Abstract
{
    public interface ICountryService
    {
        public List<CountryDTO> Get();
        public CountryDTO? GetById(int id);
        public void Update(CountryDTO countryDTO);
        public void Delete(int id);
        public CountryDTO Add(CountryDTO countryDTO);
    }
}
