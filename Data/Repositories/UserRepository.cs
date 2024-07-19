using Data.Persistence;
using Data.Repositories.Generic;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Data.Repositories
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
                .Where(p => p.Id == id)
                .Select(p => new UserModel(p.Id,p.Name!,p.Nickname!,p.Photo!,p.Email!,p.Password!))
                .FirstOrDefaultAsync();

            return user!;
        }
    }
}
