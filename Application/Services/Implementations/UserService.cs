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
            var users = await _userRepository.GetAllAsync();

            return Message.Response(
                codeResponse: CodeEnum.OK,
                message: Operation.GET_ALL,
                isOperationSuccess: true,
                results: users.ToDTO(),
                result: null);
        }

        public async Task<APIResponse<UserOutputDTO>> CreateUser(UserCreateDTO userDTO)
        {
            var usuarioExists = await _userRepository.ExistsAsync(p => p.Nickname == userDTO.NickName);

            if (usuarioExists) return Message.Response<UserOutputDTO>(CodeEnum.BAD, Operation.USER_EXISTS, false, [], null);

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

        public async Task<APIResponse<UserOutputDTO>> DeleteUser(UserDeleteDTO userDelete)
        {
            var user = await _userRepository.GetUserAsync(p => p.Nickname == userDelete.Nickname);

            bool isPasswordValid = HashPassword.VerifyPasswordHash(userDelete.Password, user.Password!);

            if (!isPasswordValid)
                return Message.Response<UserOutputDTO>(CodeEnum.BAD, Operation.DELETE_FAILED, false, [], null);

            _userRepository.Delete(user);

            await _userRepository.SaveChangesAsync();

            return Message.Response<UserOutputDTO>(
                codeResponse: CodeEnum.OK,
                message: Operation.DELETE_RECORD,
                isOperationSuccess: true,
                results: [],
                null);
        }

        public async Task<APIResponse<UserOutputDTO>> User(Guid userId)
        {
            var user = await _userRepository.GetUserAsync(p => p.Id == userId);

            return Message.Response(
                codeResponse: CodeEnum.OK,
                message: Operation.GET_SPECIFY,
                isOperationSuccess: true,
                results: [],
                user.ToDTO());
        }
    }
}
