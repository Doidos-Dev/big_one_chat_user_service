using Data.Persistence;
using Data.Repositories.Generic;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
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
                .Select(p => new UserModel(p.Id, p.Name!, p.Nickname!, p.PhotoUrl!, p.Status))
                .ToListAsync();

            return users;
        }

        public async Task<UserModel> GetUserAsync(Expression<Func<UserModel,bool>> predicate)
        {
            var user = await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .Select(p => new UserModel(p.Id, p.Name!, p.Nickname!, p.PhotoUrl!, p.Email!, p.Password!))
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
