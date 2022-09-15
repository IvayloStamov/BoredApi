using System.Collections.Generic;
using System.Threading.Tasks;
using BoredApi.Controllers;
using BoredApi.Dtos;
using BoredApi.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BoredApi.Test.Controllers
{
    public class ParticipateInRandomActivity
    {
        [Fact]
        public async Task When_Single_New_User()
        {
            //user controller => register => get registered user
            //userController => get registered user
            //activityController => get random activity
            //activityController => start activity
            //activityController => end activity
            //??? -activityController => list all user activities
            // 
        }
        
        [Fact]
        public async Task When_User_In_Group_Of_Ten()
        {
            //user controller => register => get registered user
            //userController => get registered user
            //activityController => get random activity
            //activityController => start activity
            //activityController => end activity
            //??? -activityController => list all user activities
            // 
        }
    }
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
