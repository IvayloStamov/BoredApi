using BoredApi.Data;
using BoredApi.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BoredApi.Services
{
    public class AddUserService
    {
        private readonly BoredApiContext _boredApiContext;

        public AddUserService(BoredApiContext boredApiContext)
        {
            _boredApiContext = boredApiContext;
        }

        public async Task<ActionResult<List<User>>> AddUserToTheDatabase(UserDto user)
        {
            string username = user.Username;

            var userCheck = _boredApiContext.Users.FirstOrDefault(x => x.Username == username);
            if (userCheck != null)
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
