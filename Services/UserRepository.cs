using BoredApi.Data;
using BoredApi.Data.Models;
using BoredApi.Data.Models.Exceptions;
using BoredApi.Services.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BoredApi.Services
{
    public class UserRepository : IUserService
    {
        // TODO: Rename the services that are not services to repositories 
        private readonly BoredApiContext _boredApiContext;

        public UserRepository(BoredApiContext boredApiContext)
        {
            _boredApiContext = boredApiContext;
        }

        public async Task<ActionResult<List<UserDto>>> AddUserToTheDatabaseAsync(UserDto user)
        {
            string username = user.Username;

            var userToCheck = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Username == user.Username);
            if (userToCheck != null)
            {
                throw new UserAlreadyExistsException(user.Username);
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

        public async Task<ActionResult<List<UserWithActivitiesDto>>> ShowAllRequestsAsync(int groupId)
        {
            var returnResult = await _boredApiContext.JoinActivityRequests
                .Include(x => x.User)
                .ThenInclude(y => y.UserGroups.Where(z => z.GroupId == groupId))
                .Select(x => new UserWithActivitiesDto()
                {
                    Username = x.User.Username,
                    Requests = x.User.JoinActivityRequests.Select(a => new ActivityDto()
                    {
                        ActivityName = a.Name,
                        Status = a.HasAccepted
                    }).ToList()
                }).ToListAsync();

            return returnResult;
        }
    }
}
