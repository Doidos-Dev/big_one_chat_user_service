using Application.DTOs.Input;
using Application.DTOs.Output;
using Application.Responses;
using Application.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

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
            var userCreate = await _userService.CreateUser(userCreateDTO);

            return userCreate;
        }
        [HttpPut]
        public async Task<ActionResult<APIResponse<UserOutputDTO>>> Put(UserUpdateDTO userUpdateDTO)
        {
            var userUpdate = await _userService.UpdateUser(userUpdateDTO);

            return userUpdate;
        }
        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<APIResponse<UserOutputDTO>>> Delete(Guid id)
        {
            var userDelete = await _userService.DeleteUser(id);

            return userDelete;
        } 
    }
}
