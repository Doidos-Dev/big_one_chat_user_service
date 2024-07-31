using Domain.Enums;

namespace Application.DTOs.Output
{
    public record UserOutputDTO(
        Guid Id,
        string Name,
        string Nickname,
        StatusEnum Status,
        string Photo,
        string Email,
        string Password,
        SettingsOutputDTO Settings,
        DateTimeOffset? LastSeen,
        DateTimeOffset CreatedDate);
}
