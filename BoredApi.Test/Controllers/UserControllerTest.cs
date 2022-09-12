using BoredApi.Controllers;
using BoredApi.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Models;
using Xunit;

namespace BoredApi.Test.Controllers
{
    public class UserControllerTest
    {
        [Fact]
        public async Task Get_ShouldReturnAllUsersAsAListOfUserDtosAsync()
        {
            //Arrange

            var userController = new UserController(UserServiceMock.Instance);

            //Act

            var actionResult = await userController.Get();

            //Assert

            var viewResult = Assert.IsType<ActionResult<List<UserDto>>>(actionResult);
            var model = Assert.IsAssignableFrom<List<UserDto>>(
                viewResult.Value);
            Assert.Single(model);
        }
        [Fact]
        public async Task AddUser_ShouldAddANewUserAsync()
        {
            //Arrange 
            // DI Container for registering the services / Mock only the DB (In-Memory) and use the services directly /
            // Different DB for each and every test
            var userController = new UserController(UserServiceMock.Instance);
            var userDto = new UserDto
            {
                FirstName = "firstName",
                LastName = "lastName",
                Username = "username"
            };
            //Act

            var actionResult = await userController.Get();

            //Assert

            var viewResult = Assert.IsType<ActionResult<List<UserDto>>>(actionResult);
            var model = Assert.IsAssignableFrom<List<UserDto>>(
                viewResult.Value);
            Assert.Equal(1, model.Count);
        }
    }
}
