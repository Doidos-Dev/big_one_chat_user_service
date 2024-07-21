using Application.DTOs.Input;
using Application.DTOs.Output;
using Domain.Entities;

namespace Application.Mappings
{
    public static class SettingsMapping
    {
        public static SettingsModel ToEntityInputUpdate(this SettingsUpdateDTO dto)
        {
            return new SettingsModel(dto.Id,dto.UserId,dto.IsVisibleStatus,dto.IsVisibleLastSeen,dto.IsVisibleMessageSeen);
        }

        public static SettingsOutputDTO ToDTO(this SettingsModel entity) 
        {
            return new SettingsOutputDTO(entity.Id, entity.UserId, entity.IsVisibleStatus, entity.IsVisibleLastSeen, entity.IsVisibleMessageSeen);
        }

        public static IEnumerable<SettingsOutputDTO> ToDTO(this IEnumerable<SettingsModel> entity)
        {
            return entity.Select(p => p.ToDTO());
        }
    }
}
