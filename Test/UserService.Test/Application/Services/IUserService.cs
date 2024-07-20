using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserService.Test.Application.DTOs.Input;
using UserService.Test.Application.DTOs.Output;
using UserService.Test.Application.Responses;

namespace UserService.Test.Application.Services
{
    public interface IUserService
    {
        Task<APIResponse<UserOutputDTO>> AllUsers();
        Task<APIResponse<UserOutputDTO>> User(Guid userId);
        Task<APIResponse<UserOutputDTO>> CreateUser(UserCreateDTO userDTO);
        Task<APIResponse<UserOutputDTO>> UpdateUser(UserUpdateDTO userDTO);
        Task<APIResponse<UserOutputDTO>> RemoveUser(UserDeleteDTO userDTO);
    }
}
