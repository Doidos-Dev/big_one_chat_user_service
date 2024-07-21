using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserService.Test.Domain.Entities;
using UserService.Test.Domain.Interfaces;
using UserService.Test.Infraestructure.Data.Persistence;
using UserService.Test.Infraestructure.Data.Repositories.Generic;

namespace UserService.Test.Infraestructure.Data.Repositories
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
