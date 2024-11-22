using DAL.Abstract;
using Entities.Models;

namespace DAL.Concrete
{
    public class CountryRepository:GenericRepository<Country>, ICountryRepository
    {
    }
}
