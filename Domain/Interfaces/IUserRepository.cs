using Domain.Entities;
using Domain.Interfaces.Generic;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel> GetUserAsync(Expression<Func<UserModel,bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<UserModel, bool>> predicate);
    }
}