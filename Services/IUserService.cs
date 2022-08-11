using BoredApi.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BoredApi.Services
{
    public interface IUserService
    {
        public Task<ActionResult<List<UserDto>>> GetAllUsersAsync();
        public Task<ActionResult<List<UserDto>>> AddUserToTheDatabaseAsync(UserDto user);
        public Task<ActionResult<string>> GetUsersBasedOnActivityType(string typeOfActivity);
    }
}
