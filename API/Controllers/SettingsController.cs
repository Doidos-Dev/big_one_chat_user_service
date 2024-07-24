using Application.DTOs.Input;
using Application.DTOs.Output;
using Application.Responses;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly ISettingsService _settingsService;

        public SettingsController(ISettingsService settingsService)
        {
            _settingsService = settingsService;
        }

        [HttpGet("{settingsId:guid}")]
        public async Task<ActionResult<APIResponse<SettingsOutputDTO>>> Get(Guid settingsId)
        {
            return await _settingsService.Settings(settingsId);
        }

        [HttpGet("user/{userId:guid}")]
        public async Task<ActionResult<APIResponse<SettingsOutputDTO>>> GetByUserId(Guid userId)
        {
            return await _settingsService.SettingsByUserId(userId);
        }

        [HttpPut]
        public async Task<ActionResult<APIResponse<SettingsOutputDTO>>> Put(SettingsUpdateDTO settingsDTO)
        {
            return await _settingsService.UpdateSettings(settingsDTO);
        }
    }
}
