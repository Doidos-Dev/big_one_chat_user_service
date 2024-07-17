using Domain.Entities;
using Domain.Interfaces.Generic;

namespace Domain.Interfaces
{
    public interface IUserRepository : IRepository<UserModel>
    {
        Task<IEnumerable<UserModel>> GetAll();
        Task<UserModel> GetUser(Guid id);
    }
}
