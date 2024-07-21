using Application.Responses;
using Application.DTOs.Output;
using Application.DTOs.Input;

namespace Application.Services
{
    public interface ISettingsService
    {
        Task<APIResponse<SettingsOutputDTO>> Settings(Guid settingsId);
        Task<APIResponse<SettingsOutputDTO>> SettingsByUserId(Guid userId);
        Task<APIResponse<SettingsOutputDTO>> UpdateSettings(SettingsUpdateDTO settingsDTO);
    }
}
