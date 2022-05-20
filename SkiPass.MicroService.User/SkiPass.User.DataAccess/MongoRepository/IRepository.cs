using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace SkiPass.User.DataAccess.MongoRepository
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();
        IQueryable<T> GetAll(Expression<Func<T, bool>> expression);
        int Count();
        int Count(Expression<Func<T, bool>> expression);
        T Get(Expression<Func<T, bool>> expression);
        void Add(T entity);
        void Update(T entity);
        void Update(Expression<Func<T, bool>> expression, T entity);
        void Delete(Expression<Func<T, bool>> expression, bool forceDelete);
    }
}
