using UserService.Test.Application.DTOs.Input;
using UserService.Test.Application.DTOs.Output;
using UserService.Test.Application.Responses;
using UserService.Test.Domain.Entities;
using UserService.Test.Domain.Interfaces;
using UserService.Test.Application.Helper;
using UserService.Test.Application.Enums;
using UserService.Test.Application.Mappings;

namespace UserService.Test.Application.Services.Implementations
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepository _settingsRepository;

        public SettingsService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<APIResponse<SettingsOutputDTO>> AllSettings()
        {
            var settings = await _settingsRepository
                .GetAllAsync();

            return Message.Response<SettingsOutputDTO>(
                codeResponse: CodeEnum.OK,
                message: Operation.GET_ALL,
                isOperationSuccess: true,
                results: settings.ToDTO(),
                null
                );
                
        }

        public async Task<APIResponse<SettingsOutputDTO>> Settings(Guid settingsId)
        {
            var settings = await _settingsRepository.GetSettingsAsync(x => x.Id == settingsId);

            if (settings is null)
                return Message.Response<SettingsOutputDTO>(
                    codeResponse: CodeEnum.NOT_FOUND,
                    message: Operation.GET_SPECIFY_SETTINGS_NOTFOUND,
                    isOperationSuccess: false,
                    results: [],
                    null
                    );

            return Message.Response<SettingsOutputDTO>(
                codeResponse: CodeEnum.OK,
                message: Operation.GET_SPECIFY,
                isOperationSuccess: true,
                results: [],
                settings.ToDTO()
                );

        }

        public async Task<APIResponse<SettingsOutputDTO>> SettingsByUserId(Guid userId)
        {
            var settings = await _settingsRepository.GetSettingsAsync(x => x.UserId == userId);

            if (settings is null)
                return Message.Response<SettingsOutputDTO>(
                    codeResponse: CodeEnum.NOT_FOUND,
                    message: Operation.GET_SPECIFY_NOTFOUND,
                    isOperationSuccess: false,
                    results: [],
                    null
                    );

            return Message.Response<SettingsOutputDTO>(
               codeResponse: CodeEnum.OK,
               message: Operation.GET_SPECIFY,
               isOperationSuccess: true,
               results: [],
               settings.ToDTO()
               );

        }

        public async Task<APIResponse<SettingsOutputDTO>> UpdateSettings(SettingsUpdateDTO settingsDTO)
        {
            var entity = await _settingsRepository.GetSettingsAsync(x => x.UserId == settingsDTO.UserId
            && x.Id == settingsDTO.Id);

            if (entity is null)
                return Message.Response<SettingsOutputDTO>(
                    codeResponse: CodeEnum.NOT_FOUND,
                    message: Operation.GET_SPECIFY_SETTINGS_NOTFOUND,
                    isOperationSuccess: false,
                    results: [],
                    null
                    );

            _settingsRepository.DetacheTrackingModel(entity);

            entity = settingsDTO.ToEntityInputUpdate();

            _settingsRepository.Update(entity);
            await _settingsRepository.SaveChangesAsync();

            return Message.Response<SettingsOutputDTO>(
                codeResponse: CodeEnum.OK,
                message: Operation.UPDATE_RECORD,
                isOperationSuccess: true,
                results: [],
                null);

        }

        public async Task<APIResponse<SettingsOutputDTO>> RemoveSettings(Guid userId)
        {
            var settings = await _settingsRepository.GetSettingsAsync(x => x.UserId == userId);

            if (settings is null)
                return Message.Response<SettingsOutputDTO>(
                    codeResponse: CodeEnum.NOT_FOUND,
                    message: Operation.GET_SPECIFY_SETTINGS_NOTFOUND,
                    isOperationSuccess: false,
                    results: [],
                    null
                    );

            return Message.Response<SettingsOutputDTO>(
                codeResponse: CodeEnum.OK,
                message: Operation.DELETE_RECORD,
                isOperationSuccess: true,
                results: [],
                null);

        }
    }
}
