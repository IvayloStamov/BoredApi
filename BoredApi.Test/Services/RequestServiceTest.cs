using BoredApi.Data.DataModels.Enums;
using BoredApi.Data.Models;
using BoredApi.Services;
using BoredApi.Services.ViewModels;
using BoredApi.Test.Mocks;
using Xunit;

namespace BoredApi.Test.Services
{
    public class RequestServiceTest
    {
        public RequestServiceTest()
        {

        }
        [Fact]
        public async Task ChangeRequestStatusAsync_ShouldChangeTheStatusOfTheRequest1()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };

            var groupOne = new Group { Id = 1, OwnerId = 1 };
            var groupActiviryOne = new GroupActivity { Id = 1 ,
                GroupId = 1, ActivityId = 1, Status = Status.Pending, StartDate = DateTime.Now};
            var joinRequestOne = new JoinActivityRequest { UserId = 1, GroupActivityId = 1, HasAccepted = Status.Pending };
            data.Users.Add(userOne);
            data.Users.Add(userTwo);
            data.Groups.Add(groupOne);
            
            data.GroupActivities.Add(groupActiviryOne);
            await data.SaveChangesAsync();
            data.JoinActivityRequests.Add(joinRequestOne);

            await data.SaveChangesAsync();
            var groupService = new GroupService(data);
            await groupService.AddUserToGroupAsync(1, 2, 1);
            var requestService = new RequestService(data);
            var changeRequestDto = new ChangeRequestStatusDto { Status = Status.Accepted };

            //Act

            var result = await requestService.ChangeRequestStatusAsync(2, 1, changeRequestDto);

            //Assert

            Assert.Equal(Status.Accepted, result.Value.HasAccepted);
        }
        [Fact]
        public async Task ChangeRequestStatusAsync_ShouldChangeTheStatusOfTheRequest()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };

            var groupOne = new Group { Id = 1, OwnerId = 1 };
           
            var joinRequestOne = new JoinActivityRequest { UserId = 1, GroupActivityId = 1, HasAccepted = Status.Pending };
            data.Users.Add(userOne);
            data.Users.Add(userTwo);
            data.Groups.Add(groupOne);

            data.GroupActivities.Add(groupActiviryOne);
            await data.SaveChangesAsync();
            data.JoinActivityRequests.Add(joinRequestOne);

            await data.SaveChangesAsync();
            var groupService = new GroupService(data);
            await groupService.AddUserToGroupAsync(1, 2, 1);
            var requestService = new RequestService(data);
            var changeRequestDto = new ChangeRequestStatusDto { Status = Status.Accepted };

            //Act

            var result = await requestService.ChangeRequestStatusAsync(2, 1, changeRequestDto);

            //Assert

            Assert.Equal(Status.Accepted, result.Value.HasAccepted);
        }
    }
}
