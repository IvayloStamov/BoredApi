using BoredApi.Data;
using BoredApi.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BoredApi.Services
{
    public class UserService : IUserService
    {
        private readonly BoredApiContext _boredApiContext;

        public UserService(BoredApiContext boredApiContext)
        {
            _boredApiContext = boredApiContext;
        }

        public async Task<ActionResult<List<UserDto>>> AddUserToTheDatabaseAsync(UserDto user)
        {
            string username = user.Username;

            var userToCheck = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Username == user.Username);
            if (userToCheck != null)
            {
                throw new Exception($"A user with the username ({username}) already exists.");
            }

            User newUser = new User
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            await _boredApiContext.AddAsync(newUser);
            await _boredApiContext.SaveChangesAsync();

            var returnResult = await _boredApiContext.Users
                .Select(x => new UserDto()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.Username
                })
                .ToListAsync();
            return returnResult;
        }

        public async Task<ActionResult<List<UserDto>>> GetAllUsersAsync()
        {
            var returnResult = await _boredApiContext.Users
                .Select(x => new UserDto()
                {
                    FirstName = x.FirstName,
                    LastName = x.LastName,
                    Username = x.Username
                })
                .ToListAsync();

            return returnResult;
        }

        public async Task<ActionResult<string>> GetUsersBasedOnActivityType(string typeOfActivity)
        {
            var Url = $"http://www.boredapi.com/api/activity?type={typeOfActivity}";

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(Url);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            BoredApiResponse? boredApi = Newtonsoft.Json.JsonConvert.DeserializeObject<BoredApiResponse>(jsonString);

            return boredApi.Activity;
        }
    }
}
