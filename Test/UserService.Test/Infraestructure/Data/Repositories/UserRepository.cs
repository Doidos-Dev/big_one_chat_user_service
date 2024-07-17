
using Microsoft.EntityFrameworkCore;
using UserService.Test.Domain.Entities;
using UserService.Test.Domain.Interfaces;
using UserService.Test.Infraestructure.Data.Persistence;
using UserService.Test.Infraestructure.Data.Repositories.Generic;

namespace UserService.Test.Infraestructure.Data.Repositories
{
    public class UserRepository : Repository<UserModel>, IUserRepository
    {
        public UserRepository(DatabaseContext databaseContext) : base(databaseContext)
        {
        }

        public async Task<IEnumerable<UserModel>> GetAll()
        {
            var users = await _dbSet
               .AsNoTracking()
               .Select(p => new UserModel(p.Id, p.Name!, p.Nickname!, p.Photo!, p.Status))
               .ToListAsync();

            return users;
        }

        public async Task<UserModel> GetUser(Guid id)
        {
            var user = await _dbSet
                .AsNoTracking()
                .Select(p => new UserModel(p.Id, p.Name!, p.Nickname!, p.Photo!, p.Email!, p.Password!))
                .FirstAsync();

            return user;
        }
    }
}
