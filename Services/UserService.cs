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

        public async Task<ActionResult<List<User>>> AddUserToTheDatabase(UserDto user)
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

            return await _boredApiContext.Users.ToListAsync();
        }
    }
}
