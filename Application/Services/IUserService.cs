﻿using Application.DTOs.Input;
using Application.DTOs.Output;
using Application.Responses;

namespace Application.Services
{
    public interface IUserService
    {
        Task<APIResponse<UserOutputDTO>> AllUsers(int page);
        Task<APIResponse<UserOutputDTO>> User(Guid userId);
        Task<APIResponse<UserOutputDTO>> CreateUser(UserCreateDTO userDTO);
        Task<APIResponse<UserOutputDTO>> UpdateUser(UserUpdateDTO userDTO);
        Task<APIResponse<UserOutputDTO>> RemoveUser(UserDeleteDTO userDTO);
    }
}