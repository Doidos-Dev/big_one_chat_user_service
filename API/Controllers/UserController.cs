using Application.DTOs.Input;
using Application.DTOs.Output;
using Application.Responses;
using Application.Services;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService,
        IValidator<UserCreateDTO> userCreateValidator,
        IValidator<UserUpdateDTO> userUpdateValidator) : ControllerBase
    {
        private readonly IUserService _userService = userService;
        private readonly IValidator<UserCreateDTO> _userCreateValidator = userCreateValidator;
        private readonly IValidator<UserUpdateDTO> _userUpdateValidator = userUpdateValidator;

        [HttpGet]
        public async Task<ActionResult<APIResponse<UserOutputDTO>>> GetAll()
        {
            var users = await _userService.AllUsers();

            return users;
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<APIResponse<UserOutputDTO>>> Get(Guid id)
        {
            var user = await _userService.User(id);

            return user;
        }

        [HttpPost]
        public async Task<ActionResult<APIResponse<UserOutputDTO>>> Post(UserCreateDTO userCreateDTO)
        {
            var validateUser = _userCreateValidator.Validate(userCreateDTO);

            if (!validateUser.IsValid)
                return BadRequest(validateUser.Errors);

            var userCreate = await _userService.CreateUser(userCreateDTO);

            if (!userCreate.IsOperationSuccess)
                return BadRequest(userCreate);

            return userCreate;
        }
        
        [HttpPut]
        public async Task<ActionResult<APIResponse<UserOutputDTO>>> Put(UserUpdateDTO userUpdateDTO)
        {
            var validateUser = _userUpdateValidator.Validate(userUpdateDTO);

            if (!validateUser.IsValid)
                return BadRequest(validateUser.Errors);

            var userUpdate = await _userService.UpdateUser(userUpdateDTO);

            return userUpdate;
        }

        [HttpDelete]
        public async Task<ActionResult<APIResponse<UserOutputDTO>>> Delete(UserDeleteDTO userDeleteDTO)
        {
            var userDelete = await _userService.RemoveUser(userDeleteDTO);

            return userDelete;
        }

    }
}
