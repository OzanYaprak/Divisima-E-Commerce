using DAL.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BL.Repositories
{
    public class SQLRepository<T> : IRepository<T> where T : class
    {
        SqlContext db;
        public SQLRepository(SqlContext _db)
        {
            db = _db;
        }

        public void Add(T entity)
        {
            db.Add(entity);
            db.SaveChanges();
        }

        public void Delete(T entity)
        {
            db.Remove(entity);
            db.SaveChanges();
        }

        public void DeleteAll(IQueryable<T> entity)
        {
            db.RemoveRange(entity);
            db.SaveChanges();
        }

        public IQueryable<T> GetAll()
        {
            return db.Set<T>();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> expression)
        {
            return db.Set<T>().Where(expression);
        }

        public T GetBy(Expression<Func<T, bool>> expression)
        {
            return db.Set<T>().FirstOrDefault(expression);
        }

        public void Update(T entity)
        {
            db.Update(entity);
            db.SaveChanges();
        }

        public void Update(T entity, params Expression<Func<T, object>>[] expressions)
        {
            if (expressions.Any()) foreach (Expression<Func<T, object>> exp in expressions) db.Entry(entity).Property(exp).IsModified = true;
            else db.Update(entity);
            db.SaveChanges();
        }
    }
}
