using DAL.Abstract;
using Entities.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class CountryRepository:GenericRepository<Country>, ICountryRepository
    {
        private readonly EcommerceDbContext _context = new EcommerceDbContext();
        private readonly DbSet<Country> _country;

        public CountryRepository(EcommerceDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _country = context.Set<Country>();
        }

        public new void Update(Country country)
        {
            _country.Update(country);
            _context.SaveChanges();
        }
    }
}
