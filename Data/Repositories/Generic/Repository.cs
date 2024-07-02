using Data.Persistence;
using Domain.Interfaces.Generic;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories.Generic
{
    public class Repository<T> : IRepository<T> where T : class
    {
        protected readonly ContextCommand _contextCommand;
        protected readonly DbSet<T> _dbSet;
        public Repository(ContextCommand contextCommand)
        {
            _contextCommand = contextCommand;
            _dbSet = _contextCommand.Set<T>();
        }
        public void Create(T entity) => _dbSet.Add(entity);
        public void Update(T entity) => _contextCommand.Entry(entity).State = EntityState.Modified;
        public void Delete(T entity) => _dbSet.Remove(entity);
        public void DisposeWrite() => _contextCommand.Dispose();
        public async Task SaveChangesAsync() => await _contextCommand.SaveChangesAsync();
    }
}
