using BoredApi.Data;
using BoredApi.Data.Models;
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
            return await _userService.AddUserToTheDatabaseAsync(user);
        }

        [HttpGet("{groupId}")]
        public async Task<ActionResult<List<UserWithActivitiesDto>>> GetUsersWithActivities(int groupId)
        {
            return await _userService.ShowAllRequestsAsync(groupId);
        }

    }
}
