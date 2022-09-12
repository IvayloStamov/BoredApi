using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BoredApi.Data;
using BoredApi.Data.Models;
using BoredApi.Dtos;
using BoredApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoredApi.Services
{
    public class UserRepository : IUserService
    {
        private readonly BoredApiContext _boredApiContext;

        public UserRepository(BoredApiContext boredApiContext)
        {
            _boredApiContext = boredApiContext;
        }

        public async Task<ActionResult<List<UserDto>>> AddUserToTheDatabaseAsync(UserDto user)
        {
            User newUser = new User
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            await _boredApiContext.AddAsync(newUser);
            await _boredApiContext.SaveChangesAsync();

            return await GetAllUsersAsync();
        }

        public async Task<List<UserDto>> GetAllUsersAsync()
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
    }
}