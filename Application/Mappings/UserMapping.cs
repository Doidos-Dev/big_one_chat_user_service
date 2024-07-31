using Application.DTOs.Input;
using Application.DTOs.Output;
using Domain.Entities;

namespace Application.Mappings
{
    public static class UserMapping
    {
        public static UserOutputDTO ToDTO(this UserModel userModel)
        {
            return new UserOutputDTO(
                userModel.Id,
                userModel.Name!,
                userModel.Nickname!,
                userModel.Status,
                userModel.PhotoUrl!,
                userModel.Email!,
                userModel.Password!,
                userModel.Settings.ToDTO(),
                userModel.LastSeen,
                userModel.CreatedDate);
        }
        public static UserModel ToEntityInputUpdate(this UserUpdateDTO dto)
        {
            return new UserModel(
                dto.Id,
                dto.Name,
                dto.NickName,
                dto.Photo,
                dto.Email,
                dto.Password);
        }

        public static IEnumerable<UserOutputDTO> ToDTO(this IEnumerable<UserModel> entity)
        {
            return entity.Select(p => p.ToDTO());
        }
    }
}