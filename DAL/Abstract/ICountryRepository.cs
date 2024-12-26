using Entities.Models;

namespace DAL.Abstract
{
    public interface ICountryRepository: IGenericRepository<Country>
    {
        public new void Update(Country country);
    }
}
