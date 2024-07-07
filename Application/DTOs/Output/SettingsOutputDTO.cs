namespace Application.DTOs.Output
{
    public record SettingsOutputDTO(
        Guid Id,
        Guid UserId,
        bool IsVisibleStatus,
        bool IsVisibleLastSeen,
        bool IsVisibleMessageSeen);
}
