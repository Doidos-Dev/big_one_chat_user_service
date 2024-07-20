namespace Domain.Interfaces.Generic
{
    public interface IRepository<T>
    {
        void Create(T entity);
        void Update(T entity);
        void Delete(T entity);
        Task SaveChangesAsync();
        void Dispose();
    }
}
