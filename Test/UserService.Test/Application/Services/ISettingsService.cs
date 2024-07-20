using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using UserService.Test.Application.DTOs.Input;
using UserService.Test.Application.DTOs.Output;
using UserService.Test.Application.Responses;
using UserService.Test.Domain.Entities;

namespace UserService.Test.Application.Services
{
    public interface ISettingsService
    {
        Task<APIResponse<SettingsOutputDTO>> AllSettings();
        Task<APIResponse<SettingsOutputDTO>> Settings(Guid settingsId);
        Task<APIResponse<SettingsOutputDTO>> SettingsByUserId(Guid userId);
        Task<APIResponse<SettingsOutputDTO>> UpdateSettings(SettingsUpdateDTO settingsDTO);
        Task<APIResponse<SettingsOutputDTO>> RemoveSettings(Guid userId);
    }
}
