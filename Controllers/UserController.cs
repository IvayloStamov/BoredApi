using BoredApi.Data;
using BoredApi.Data.Models;
using BoredApi.Data.Models.Exceptions;
using BoredApi.Services;
using BoredApi.Services.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BoredApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService addUserService)
        {
            _userService = addUserService;
        }
        [HttpGet]
        public async Task<ActionResult<List<UserDto>>> Get()
        {
            
            return await _userService.GetAllUsersAsync();
        }

        [HttpPost]
        public async Task<ActionResult<List<UserDto>>> AddUser(UserDto user)
        {
            try
            {
                return await _userService.AddUserToTheDatabaseAsync(user);
            }
            catch (UserAlreadyExistsException)
            {
                return BadRequest($"User with the same username {user.Username} already exists.");
            }
        }
    }
}
