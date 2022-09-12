using System.Collections.Generic;
using System.Threading.Tasks;
using BoredApi.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace BoredApi.Services.Interfaces
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetAllUsersAsync();
        public Task<ActionResult<List<UserDto>>> AddUserToTheDatabaseAsync(UserDto user);
    }
}
