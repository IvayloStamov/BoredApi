using BoredApi.Data.Database;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BoredApi.Services
{
    public class AddUser
    {
        DataContext context = new DataContext();

        public async Task<ActionResult<List<User>>> AddUserToTheDatabase(UserDto user)
        {
            string username = user.Username;

            var userCheck = context.Users.FirstOrDefault(x => x.Username == username);
            if (userCheck != null)
            {
                throw new Exception($"A user with the userhame ({username}) already exists.");
            }

            User newUser = new User
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            await context.AddAsync(newUser);
            await context.SaveChangesAsync();

            return await context.Users.ToListAsync();
        }
    }
}
