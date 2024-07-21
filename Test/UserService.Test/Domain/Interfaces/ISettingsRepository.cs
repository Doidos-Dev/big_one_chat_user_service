using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserService.Test.Domain.Entities;
using UserService.Test.Domain.Interfaces.Generic;

namespace UserService.Test.Domain.Interfaces
{
    public interface ISettingsRepository : IRepository<SettingsModel>
    {
        Task<IEnumerable<SettingsModel>> GetAllAsync();
        Task<SettingsModel> GetSettingsAsync(Expression<Func<SettingsModel, bool>> predicate);
        Task<SettingsModel> GetSettingsAsNoTrackingAsync(Expression<Func<SettingsModel, bool>> predicate);
        void DetacheTrackingModel(SettingsModel entity);
    }
}
