using Domain.Entities;
using Domain.Interfaces.Generic;
using System.Linq.Expressions;

namespace Domain.Interfaces
{
    public interface ISettingsRepository:IRepository<SettingsModel>
    {
        Task<IEnumerable<SettingsModel>> GetAllAsync();
        Task<SettingsModel> GetSettingsAsync(Expression<Func<SettingsModel, bool>> predicate);
        Task<SettingsModel> GetSettingsAsNoTrackingAsync(Expression<Func<SettingsModel, bool>> predicate);
        void DetacheTrackingModel(SettingsModel entity);
    }
}
