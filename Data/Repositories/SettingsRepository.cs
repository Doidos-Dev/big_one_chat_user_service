using Data.Persistence;
using Data.Repositories.Generic;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class SettingsRepository : Repository<SettingsModel>,ISettingsRepository
    {
        public SettingsRepository(DatabaseContext contextCommand) : base(contextCommand){}
    }
}
