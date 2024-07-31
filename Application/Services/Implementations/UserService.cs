using Application.DTOs.Input;
using Application.DTOs.Output;
using Application.Enums;
using Application.Helper;
using Application.Mappings;
using Application.Responses;
using Application.Security;
using Domain.Entities;
using Domain.Enums;
using Domain.Interfaces;
using Microsoft.Extensions.Configuration;

namespace Application.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IConfiguration _configuration;
        private readonly string _publicKey;
        private readonly string _privateKey;
        private readonly ICriptographyAss _criptography;
        public UserService(
            IUserRepository userRepository,
            IConfiguration configuration,
            ICriptographyAss criptography)
        {
            _userRepository = userRepository;
            _configuration = configuration;
            _criptography = criptography;
            _publicKey = Environment.GetEnvironmentVariable("USER_SERVICE_CRIPTO_PARTIAL_PUBLICK") ?? _configuration["CRIPO_PARTIAL:PublicKey"]!;
            _privateKey = Environment.GetEnvironmentVariable("USER_SERVICE_CRIPTO_PARTIAL_SECRETK") ?? _configuration["CRIPO_PARTIAL:SecretKey"]!;
        }

        public async Task<APIResponse<UserOutputDTO>> AllUsers(int page)
        {
            IEnumerable<UserModel> usersCripto = await _userRepository.GetAllAsync(page);

            IEnumerable<UserOutputDTO> usersConverter = usersCripto
                .Select(userModel => new UserOutputDTO(
                userModel.Id,
                _criptography.Decrypt(userModel.Name!, _privateKey),
                _criptography.Decrypt(userModel.Nickname!, _privateKey),
                userModel.Status,
                _criptography.Decrypt(userModel.PhotoUrl!, _privateKey),
                userModel.Email,
                userModel.Password,
                userModel.Settings!.ToDTO(),
                userModel.LastSeen,
                userModel.CreatedDate));

            return Message.Response(
                codeResponse: CodeEnum.OK,
                message: Operation.GET_ALL,
                isOperationSuccess: true,
                results: usersConverter,
                result: null);
        }

        public async Task<APIResponse<UserOutputDTO>> CreateUser(UserCreateDTO userDTO)
        {

            bool userExist = await _userRepository
                .ExistsAsync(x => x.Nickname == _criptography.Encrypt(userDTO.NickName, _publicKey));

            if (userExist)
                return Message.Response<UserOutputDTO>(CodeEnum.BAD, Operation.USER_EXISTS, false, [], null);

            var usuarioEntity = new UserModel(
                Guid.NewGuid(),
                _criptography.Encrypt(userDTO.Name, _publicKey),
                _criptography.Encrypt(userDTO.NickName, _publicKey),
                StatusEnum.Offline,
                _criptography.Encrypt(userDTO.Photo, _publicKey),
                _criptography.Encrypt(userDTO.Email, _publicKey),
                _criptography.Encrypt(HashPassword.CreatePasswordHash(userDTO.Password), _publicKey),
                null,
                DateTimeOffset.UtcNow);

            _userRepository.Create(usuarioEntity);

            await _userRepository.SaveChangesAsync();

            return Message.Response<UserOutputDTO>(
                codeResponse: CodeEnum.CREATED,
                message: Operation.CREATE_RECORD,
                isOperationSuccess: true,
                results: [],
                null);
        }

        public async Task<APIResponse<UserOutputDTO>> User(Guid userId)
        {
            var user = await _userRepository.GetUserAsNoTrackingAsync(x => x.Id == userId);

            UserOutputDTO userConverter = new(
                user.Id,
                _criptography.Decrypt(user.Name!, _privateKey),
                _criptography.Decrypt(user.Nickname!, _privateKey),
                user.Status,
                _criptography.Decrypt(user.PhotoUrl!, _privateKey),
                user.Email,
                user.Password!,
                user.Settings.ToDTO(),
                user.LastSeen,
                user.CreatedDate);

            if (user is { })
            {
                return Message.Response<UserOutputDTO>(
               codeResponse: CodeEnum.NOT_FOUND,
               message: Operation.GET_SPECIFY_NOTFOUND,
               isOperationSuccess: false,
               results: [],
               null);
            }

            return Message.Response(
                codeResponse: CodeEnum.OK,
                message: Operation.GET_SPECIFY,
                isOperationSuccess: true,
                results: [],
                userConverter);
        }

        public async Task<APIResponse<UserOutputDTO>> UpdateUser(UserUpdateDTO userDTO)
        {
            var userRecovery = await _userRepository.GetUserAsNoTrackingAsync(p => p.Id != userDTO.Id && p.Nickname == userDTO.NickName);

            if (userRecovery is { })
                return Message.Response<UserOutputDTO>(CodeEnum.BAD, Operation.NICKNAME_FOUND, false, [], null);

            string newPassword = HashPassword.VerifyPasswordHash(_criptography.Encrypt(userDTO.Password, _publicKey), userRecovery.Password) ? userDTO.Password : _criptography.Encrypt(HashPassword.CreatePasswordHash(userDTO.Password), _publicKey);

            var usuarioUpdate = new UserModel(
                userDTO.Id,
                _criptography.Encrypt(userDTO.Name, _publicKey),
                _criptography.Encrypt(userDTO.NickName, _publicKey),
                _criptography.Encrypt(userDTO.Photo, _publicKey),
                _criptography.Encrypt(userDTO.Email, _publicKey),
                newPassword
                );

            _userRepository.Update(usuarioUpdate);

            await _userRepository.SaveChangesAsync();

            return Message.Response<UserOutputDTO>(
                codeResponse: CodeEnum.OK,
                message: Operation.UPDATE_RECORD,
                isOperationSuccess: true,
                results: [],
                null);
        }

        public async Task<APIResponse<UserOutputDTO>> RemoveUser(UserDeleteDTO userDTO)
        {
            var entity = await _userRepository
                .GetUserTrackingAsync(x => x.Nickname == _criptography.Encrypt(userDTO.Nickname, _publicKey));

            if (entity is null)
                return Message.Response<UserOutputDTO>(
                    codeResponse: CodeEnum.NOT_FOUND,
                    message: Operation.GET_SPECIFY_NOTFOUND,
                    isOperationSuccess: false,
                    results: [],
                    null
                    );

            bool isPasswordValid = HashPassword.VerifyPasswordHash(userDTO.Password, entity.Password!);

            if (!isPasswordValid)
                return Message.Response<UserOutputDTO>(CodeEnum.BAD, Operation.DELETE_FAILED, false, [], null);

            _userRepository.Delete(entity);

            await _userRepository.SaveChangesAsync();

            return Message.Response<UserOutputDTO>(
                codeResponse: CodeEnum.OK,
                message: Operation.DELETE_RECORD,
                isOperationSuccess: true,
                results: [],
                null
                );
        }

        static bool VerifyExitsUserOnline(string nickname)
        {
            return true;
        }
    }
}
