using Domain.Entities;
using Domain.Interfaces.Generic;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<IEnumerable<UserModel>> GetAllAsync(int page);
        Task<UserModel> GetUserAsNoTrackingAsync(Expression<Func<UserModel, bool>> predicate);
        Task<UserModel> GetUserTrackingAsync(Expression<Func<UserModel, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<UserModel, bool>> predicate);
    }
}