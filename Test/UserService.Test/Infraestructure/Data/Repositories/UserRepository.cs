
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
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

        public async Task<IEnumerable<UserModel>> GetAllAsync()
        {
            var users = await _dbSet
               .AsNoTracking()
               .Select(p => new UserModel(p.Id, p.Name!, p.Nickname!, p.Photo!, p.Status))
               .ToListAsync();

            return users;
        }

        public async Task<UserModel> GetUserAsNoTrackingAsync(Expression<Func<UserModel, bool>> predicate)
        {
            var user = await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .Select(p => new UserModel(p.Id, p.Name!, p.Nickname!, p.Photo!, p.Email!, p.Password!))
                .FirstOrDefaultAsync();
                
            return user!;
        }

        public async Task<UserModel> GetUserTrackingAsync(Expression<Func<UserModel, bool>> predicate)
        {
            var user = await _dbSet
                .Where(predicate)
                .Select(p => new UserModel(p.Id, p.Name!, p.Nickname!, p.Photo!, p.Email!, p.Password!))
                .FirstOrDefaultAsync();

            return user!;
        }

        public async Task<bool> ExistsAsync(Expression<Func<UserModel, bool>> predicate)
        {
            var userExists = await _dbSet
                .AsNoTracking()
                .AnyAsync(predicate);

            return userExists;
        }
    }
}
