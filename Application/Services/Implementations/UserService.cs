using Application.DTOs.Input;
using Application.DTOs.Output;
using Application.Enums;
using Application.Helper;
using Application.Mappings;
using Application.Responses;
using Domain.Interfaces;

namespace Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public async Task<APIResponse<UserOutputDTO>> AllUsers()
        {
            var users = await _userRepository.GetAll();

            return Message.Response(
                codeResponse: CodeEnum.OK,
                message: Operation.GET_ALL,
                isOperationSuccess: true,
                results: users.ToDTO(),
                result: null);
        }

        public async Task<APIResponse<UserOutputDTO>> CreateUser(UserCreateDTO userDTO)
        {
            var usuarioEntity = userDTO.ToEntityInputInsert();

            usuarioEntity.EncryptPasswordEntity(HashPassword.CreatePasswordHash(userDTO.Password));

            _userRepository.Create(usuarioEntity);

            await _userRepository.SaveChangesAsync();

            return Message.Response<UserOutputDTO>(
                codeResponse: CodeEnum.CREATED,
                message: Operation.CREATE_RECORD,
                isOperationSuccess: true,
                results: [],
                null);
        }

        public async Task<APIResponse<UserOutputDTO>> UpdateUser(UserUpdateDTO userDTO)
        {
            var usuarioEntity = userDTO.ToEntityInputUpdate();

            _userRepository.Update(usuarioEntity);

            await _userRepository.SaveChangesAsync();

            return Message.Response<UserOutputDTO>(
                codeResponse: CodeEnum.OK,
                message: Operation.UPDATE_RECORD,
                isOperationSuccess: true,
                results: [],
                null);
        }

        public async Task<APIResponse<UserOutputDTO>> User(Guid userId)
        {
            var user = await _userRepository.GetUser(userId);

            return Message.Response(
                codeResponse: CodeEnum.OK,
                message: Operation.GET_SPECIFY,
                isOperationSuccess: true,
                results: [],
                user.ToDTO());
        }
    }
}
