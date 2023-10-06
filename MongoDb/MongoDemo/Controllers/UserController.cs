using Microsoft.AspNetCore.Mvc;
using MongoDemo.BLL.Services.Interfaces;
using MongoDemo.Common.DTOs;

namespace MongoDemo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("registerUser")]
        public async Task<IActionResult> RegisterUser([FromBody] CreateUserDto user)
        {
            await _userService.CreateUserAsync(user);
            return Ok();
        }

        [HttpGet("getUsers")]
        public ActionResult<List<UserDto>> GetUsers()
        {
            var result = _userService.GetAllUsersAsync();
            return Ok(result);
        }

        [HttpGet("getUserById")]
        public async Task<ActionResult<UserDto>> GetUserById(string userId)
        {
            var result = await _userService.FindUserByIdAsync(userId);
            return Ok(result);
        }

        [HttpPut("updateUserDetails")]
        public async Task<IActionResult> GetUserById(string userId, [FromBody] UpdateUserDto updateUser)
        {
            await _userService.UpdateUserAsync(userId, updateUser);
            return Ok();
        }

        [HttpDelete("deleteUserById")]
        public async Task<IActionResult> DeleteUserById(string userId)
        {
            await _userService.DeleteUserByIdAsync(userId);
            return Ok();
        }

        [HttpGet("getUserByFirstName")]
        public async Task<ActionResult<UserDto>> GetUserByFirstName(string firstName)
        {
            var result = await _userService.FindUserByFirstNameAsync(firstName);
            return Ok(result);
        }
    }
}
