using Data.Persistence;
using Data.Repositories.Generic;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        private readonly ContextRead _contextRead;
        public UserRepository(ContextCommand contextCommand,ContextRead contextRead) : base(contextCommand)
        {
            _contextRead = contextRead;
        }
    }
}
