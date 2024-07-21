using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserService.Test.Domain.Entities;
using UserService.Test.Domain.Interfaces.Generic;

namespace UserService.Test.Domain.Interfaces
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<IEnumerable<UserModel>> GetAllAsync();
        Task<UserModel> GetUserAsNoTrackingAsync(Expression<Func<UserModel, bool>> predicate);
        Task<UserModel> GetUserTrackingAsync(Expression<Func<UserModel, bool>> predicate);
        Task<bool> ExistsAsync(Expression<Func<UserModel, bool>> predicate);

    }
}
