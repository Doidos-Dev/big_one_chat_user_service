using Data.Persistence;
using Data.Repositories.Generic;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class UserRepository(DatabaseContext databaseContext) : Repository<UserModel>(databaseContext), IUserRepository
    {
        public async Task<IEnumerable<UserModel>> GetAllAsync(int page)
        {
            int pageSize = 20;

            var users = await _dbSet
               .AsNoTracking()
               .Include(p => p.Settings)
               .Select(p => new UserModel(p.Id, p.Name!, p.Nickname!, p.PhotoUrl!, p.Status, p.Settings!))
               .Skip((page - 1) * pageSize)
               .Take(pageSize)
               .ToListAsync();

            return users;
        }

        public async Task<UserModel> GetUserAsNoTrackingAsync(Expression<Func<UserModel, bool>> predicate)
        {
            var user = await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .Select(p => new UserModel(p.Id, p.Name!, p.Nickname!, p.PhotoUrl!, p.Email!, p.Password!))
                .FirstOrDefaultAsync();

            return user!;
        }

        public async Task<UserModel> GetUserTrackingAsync(Expression<Func<UserModel, bool>> predicate)
        {
            var user = await _dbSet
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
