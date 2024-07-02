using System.Linq.Expressions;

namespace Domain.Interfaces.Generic
{
    public interface IRepository<T>
    {
        Task<IEnumerable<T>> GetAllAsync(Expression<Func<T,bool>> expression);
        Task<T> GetAsync(Expression<Func<T,bool>> expression);
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
