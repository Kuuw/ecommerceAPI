using Entities.Models;

namespace DAL.Abstract
{
    public interface ICountryRepository: IGenericRepository<Country>
    {
        public void Update(Country country);
    }
}
