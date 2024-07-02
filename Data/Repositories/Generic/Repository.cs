using Domain.Interfaces.Generic;
using System.Linq.Expressions;

namespace Data.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        public Repository()
        {
        }

        public void Create(T entity)
        {
            throw new NotImplementedException();
        }

        public void Delete(T entity)
        {
            throw new NotImplementedException();
        }

        public void Update(T entity)
        {
            throw new NotImplementedException();
        }
    }
}
