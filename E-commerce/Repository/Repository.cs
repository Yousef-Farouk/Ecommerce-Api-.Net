
using E_commerce.Models;

namespace E_commerce.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly EcommerceContext db;

        public Repository(EcommerceContext _db)
        {
            db = _db;
        }
        public void Add(T obj)
        {
           db.Set<T>().Add(obj);
        }

        public void Delete(int id)
        {
            var obj = db.Set<T>().Find(id);
            db.Set<T>().Remove(obj);
        }

        public List<T> GetAll()
        {
            return db.Set<T>().ToList();
        }

        public T GetById(int id)
        {
            return db.Set<T>().Find(id);
        }

        public void Update(T obj)
        {
           // db.Entry(obj).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
           db.Set<T>().Update(obj);
        }

        public void Save()
        {
            db.SaveChanges();
        }

    }
}
