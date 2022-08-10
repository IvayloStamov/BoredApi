using BoredApi.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BoredApi.Services
{
    public interface IUserService
    {
        public Task<ActionResult<List<User>>> AddUserToTheDatabase(UserDto user)
    }
}
