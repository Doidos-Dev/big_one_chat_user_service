namespace Application.DTOs.Input
{
    public record SettingsUpdateDTO(
        Guid Id,
        Guid UserId,
        bool IsVisibleStatus,
        bool IsVisibleLastSeen,
        bool IsVisibleMessageSeen);
}
