using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
