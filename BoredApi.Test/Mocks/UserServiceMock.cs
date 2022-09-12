using System.Collections.Generic;
using System.Threading.Tasks;
using BoredApi.Dtos;
using BoredApi.Services;
using BoredApi.Services.Interfaces;
using Moq;

namespace BoredApi.Test.Mocks
{
    public static class UserServiceMock
    {
        public static IUserService Instance
        {
            get
            {
                var userServiceMock = new Mock<IUserService>();
                var users = new List<UserDto>()
                {
                    new UserDto()
                    {
                        FirstName = "1",
                        LastName = "1",
                        Username = "1"
                    }
                };
                userServiceMock
                    .Setup(x => x.GetAllUsersAsync())
                    .Returns(Task.FromResult(users));

                return userServiceMock.Object;
            }
        }
    }
}