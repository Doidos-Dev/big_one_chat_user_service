using Data.Persistence;
using Data.Repositories.Generic;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Data.Repositories
{
    public class SettingsRepository : Repository<SettingsModel>, ISettingsRepository
    {
        public SettingsRepository(DatabaseContext databaseContext) : base(databaseContext) { }

        public async Task<IEnumerable<SettingsModel>> GetAllAsync()
        {
            var settings = await _dbSet
                .AsNoTracking()
                .ToListAsync();

            return settings;
        }

        public async Task<SettingsModel> GetSettingsAsync(Expression<Func<SettingsModel, bool>> predicate)
        {
            var settings = await _dbSet
                .Where(predicate)
                .FirstOrDefaultAsync();

            return settings!;
        }

        public async Task<SettingsModel> GetSettingsAsNoTrackingAsync(Expression<Func<SettingsModel, bool>> predicate)
        {
            var settings = await _dbSet
                .AsNoTracking()
                .Where(predicate)
                .FirstOrDefaultAsync();

            return settings!;
        }

        public void DetacheTrackingModel(SettingsModel entity)
            => _databaseContext.Entry(entity).State = EntityState.Detached;
    }
}
