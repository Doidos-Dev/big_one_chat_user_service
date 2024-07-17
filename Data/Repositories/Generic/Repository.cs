using Data.Persistence;
using Domain.Interfaces.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DatabaseContext _databaseContext;
        protected readonly DbSet<T> _dbSet;
        public Repository(DatabaseContext databaseContext)
        {
            _databaseContext = databaseContext;
            _dbSet = _databaseContext.Set<T>();
        }
        public void Create(T entity) => _dbSet.Add(entity);
        public void Update(T entity) => _databaseContext.Entry(entity).State = EntityState.Modified;
        public void Delete(T entity) => _dbSet.Remove(entity);
        public void Dispose() => _databaseContext.Dispose();
        public async Task SaveChangesAsync() => await _databaseContext.SaveChangesAsync();
    }
}