using DAL.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DAL.Concrete
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {
        private readonly EcommerceDbContext _context = new EcommerceDbContext();
        private readonly DbSet<T> data;

        public GenericRepository()
        {
            data = _context.Set<T>();
        }

        public void Delete(T p)
        {
            data.Remove(p);
            _context.SaveChanges();
        }

        public T? GetById(int id)
        {
            return data.Find(id);
        }

        public void Insert(T p)
        {
            data.Add(p);
            _context.SaveChanges();
        }

        public List<T> List()
        {
            return data.ToList();
        }

        public void Update(T p)
        {
            _context.SaveChanges();
        }

        public List<T> Where(Func<T, bool> predicate)
        {
            return data.Where(predicate).ToList();
        }
    }
}