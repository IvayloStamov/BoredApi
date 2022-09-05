using BoredApi.Data.Models;
using BoredApi.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace BoredApi.Services
{
    public interface IUserService
    {
        public Task<List<UserDto>> GetAllUsersAsync();
        public Task<ActionResult<List<UserDto>>> AddUserToTheDatabaseAsync(UserDto user);
        public Task<ActionResult<List<UserWithActivitiesDto>>> ShowAllRequestsAsync(int groupId);
    }
}
