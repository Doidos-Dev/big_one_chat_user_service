using UserService.Test.Application.DTOs.Input;
using UserService.Test.Application.DTOs.Output; 
using UserService.Test.Domain.Entities; 
using UserService.Test.Domain.Enums; 

namespace UserService.Test.Application.Mappings
{
    public static class UserMapping
    {
        public static UserOutputDTO ToDTO(this UserModel userModel)
        {
            return new UserOutputDTO(
                userModel.Id,
                userModel.Name!,
                userModel.Nickname!,
                userModel.Status == StatusEnum.Online ? "online" : "offline",
                userModel.Photo!,
                userModel.Email!,
                userModel.Password!,
                userModel.LastSeen,
                userModel.CreatedDate);
        }
        public static UserModel ToEntityInputInsert(this UserCreateDTO dto)
        {
            return new UserModel(
                Guid.NewGuid(),
                dto.Name,
                dto.NickName,
                StatusEnum.Offline,
                dto.Photo,
                dto.Email,
                dto.Password,
                null,
                DateTime.Now.ToUniversalTime());
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
