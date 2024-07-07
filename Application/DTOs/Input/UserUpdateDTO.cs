using Domain.Enums;

namespace Application.DTOs.Input
{
    public record UserUpdateDTO(
         Guid Id,
         string Name,
         string NickName,
         string Photo,
         string Email,
         string Password);
}
