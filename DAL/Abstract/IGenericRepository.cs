namespace DAL.Abstract
{
    public interface IGenericRepository<T>
    {
        List<T> List(); 
        void Insert(T p);
        void Delete(T p);
        void Update(T p);
        T GetById(int id);
        List<T> Where(Func<T, bool> predicate);
    }
}
