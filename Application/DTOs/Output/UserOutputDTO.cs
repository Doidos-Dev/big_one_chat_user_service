namespace Application.DTOs.Output
{
    public record UserOutputDTO(
        Guid Id,
        string Name,
        string Nickname,
        string Status,
        string Photo,
        string Email,
        string Password,
        DateTime? LastSeen,
        DateTime CreatedDate);
}
