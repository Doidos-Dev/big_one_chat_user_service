using Data.Persistence;
using Data.Repositories.Generic;
using Domain.Entities;
using Domain.Interfaces;

namespace Data.Repositories
{
    public class SettingsRepository : Repository<SettingsModel>,ISettingsRepository
    {
        private readonly ContextRead _contextRead;
        public SettingsRepository(ContextCommand contextCommand,ContextRead contextRead) : base(contextCommand)
        {
            _contextRead = contextRead;
        }
    }
}
