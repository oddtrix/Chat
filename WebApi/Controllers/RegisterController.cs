using BLL.Interfaces;
using DAL.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace WebApi.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class RegisterController : ControllerBase
    {
        private readonly IUserService registerService;

        public RegisterController(IUserService registerService)
        {
            this.registerService = registerService;
        }

        [HttpGet]
        public async Task<IActionResult> GetUsers()
        {
            var users = await this.registerService.GetUsersAsync();

            return Ok(users);
        }

        [HttpGet]
        public async Task<IActionResult> GetUserById(Guid userId)
        {
            var user = await this.registerService.GetUserByIdAsync(userId);
            if (user == null)
            {
                return BadRequest(userId);
            }

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] CreateUserDTO createUserDTO)
        {
            if (string.IsNullOrEmpty(createUserDTO.UserName))
            {
                return BadRequest("Name is required");
            }

            var user = await this.registerService.CreateUserAsync(createUserDTO.UserName);

            return Ok(user);
        }

        [HttpPatch]
        public async Task<IActionResult> ChangeUserName([FromBody] ChangeUserNameDTO changeUserNameDTO)
        {
            if (string.IsNullOrEmpty(changeUserNameDTO.UserName))
            {
                return BadRequest("Name is required");
            }

            var user = await this.registerService.ChangeUserNameAsync(changeUserNameDTO);

            return Ok(user);
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteUser(Guid userId)
        {
            var deletedUser = await this.registerService.DeleteUserAsync(userId);
            return Ok(deletedUser);
        }
    }
}
