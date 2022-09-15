using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BoredApi.Dtos;
using BoredApi.Services;
using BoredApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    }
}