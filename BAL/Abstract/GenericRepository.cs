using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace BAL.Abstract
{
    public class GenericRepository<T> : IRepository<T> where T : class, new()
    {
        EcommerceDbContext context = new EcommerceDbContext();
        DbSet<T> data;
        public GenericRepository()
        {
            data = context.Set<T>();
        }

        public void Delete(T p)
        {
            data.Remove(p);
            context.SaveChanges();
        }

        public T GetById(int id)
        {
            return data.Find(id);
        }

        public void Insert(T p)
        {
            data.Add(p);
            context.SaveChanges();
        }

        public List<T> List()
        {
            return data.ToList();
        }

        public void Update(T p)
        {
            context.SaveChanges();
        }

        public List<T> Where(Func<T, bool> predicate)
        {
            return data.Where(predicate).ToList();
        }
    }
}