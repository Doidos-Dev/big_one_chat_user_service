using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Test.Domain.Entities;
using UserService.Test.Domain.Interfaces.Generic;

namespace UserService.Test.Domain.Interfaces
{
    public interface ISettingsRepository : IRepository<SettingsModel>
    {
    }
}
