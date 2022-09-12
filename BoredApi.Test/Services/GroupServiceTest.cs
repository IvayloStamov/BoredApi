using BoredApi.Data.Models;
using BoredApi.Data.Models.Exceptions;
using BoredApi.Services;
using BoredApi.Services.ViewModels;
using BoredApi.Test.Mocks;
using Models;
using Xunit;

namespace BoredApi.Test.Services
{
    public class GroupServiceTest
    {
        [Fact]
        public async Task ReturnAllGroupsAsync_ShouldReturnAllGroups()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var groupOne = new Group();
            var groupTwo = new Group();

            data.Groups.Add(groupOne);
            data.Groups.Add(groupTwo);

            await data.SaveChangesAsync();
            var groupService = new GroupService(data);

            //Act

            var result = await groupService.ReturnAllGroupsAsync();

            //Assert

            Assert.Equal(2, result.Value.Count);
        }
        [Fact]
        public async Task CreateGroupAsync_ShouldCreateNewGroup()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var user = new User { Id = 1};
            data.Users.Add(user);
            await data.SaveChangesAsync();
            var groupService = new GroupService(data);

            var newGroupDto = new GroupDto { Name = "1" };
            //Act

            await groupService.CreateGroupAsync(1, newGroupDto);
            var result = await groupService.ReturnAllGroupsAsync();
            //Assert

            Assert.Equal(1, result.Value.Count);
        }
        [Fact]
        public async Task CreateGroupAsync_ShouldThrowNewGroupAlreadyExistsExceptionIfAGroupWithTheSameNameAlreadyExists()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var user = new User { Id = 1};
            var group = new Group { Name = "1" };
            data.Users.Add(user);
            data.Groups.Add(group);

            await data.SaveChangesAsync();
            var groupService = new GroupService(data);

            var newGroupDto = new GroupDto { Name = "1" };
            // Act and Assert

            await Assert.ThrowsAsync<GroupAlreadyExistsException>(() => groupService.CreateGroupAsync(1, newGroupDto));
        }
        [Fact]
        public async Task CreateGroupAsync_ShouldThrowSuchAUserDoesNotExistExceptionWhenTheUserCreatingTheGroupDoesNotExist()
        {
            //Arrange
            using var data = DatabaseMock.Instance;

            await data.SaveChangesAsync();
            var groupService = new GroupService(data);

            var newGroupDto = new GroupDto { Name = "1" };
            // Act and Assert
            
            await Assert.ThrowsAsync<SuchAUserDoesNotExistException>(() => groupService.CreateGroupAsync(1, newGroupDto));
        }
        [Fact]
        public async Task ReturnAllUsersFromGroupAsync_ShouldReturnAllUsersFromGroup()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };
            var groupOne = new Group { Id = 1, OwnerId = 1 };
            data.Groups.Add(groupOne);
            data.Users.Add(userOne);
            data.Users.Add(userTwo);

            await data.SaveChangesAsync();
            var groupService = new GroupService(data);

            // Act 

            await groupService.AddUserToGroupAsync(1, 2, 1);
            var result = await groupService.ReturnAllUsersFromGroupAsync(1);
            //Assert
            Assert.Equal(1, result.Value.Count);
        }
        [Fact]
        public async Task ReturnAllUsersFromGroupAsync_ShouldThrowSuchAGroupDoesNotExistExceptionITheGroupDoesNotExist()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };
            var groupOne = new Group { Id = 1, OwnerId = 1 };
            data.Groups.Add(groupOne);
            data.Users.Add(userOne);
            data.Users.Add(userTwo);

            await data.SaveChangesAsync();
            var groupService = new GroupService(data);

            //Act and Assert
            await Assert.ThrowsAnyAsync<SuchAGroupDoesNotExistException>(() =>
            groupService.AddUserToGroupAsync(2, 2, 1));
        }
        [Fact]
        public async Task AddUserToGroupAsync_ShouldAddNewUserToGroup()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };

            var group = new Group { Id = 1 , OwnerId = 1};
            data.Users.Add(userOne);
            data.Users.Add(userTwo);

            data.Groups.Add(group);

            await data.SaveChangesAsync();
            
            var groupService = new GroupService(data);
            //Act

            await groupService.AddUserToGroupAsync(1, 2, 1);
            var result = await groupService.ReturnAllUsersFromGroupAsync(1);
            //Assert

            Assert.Equal(1, result.Value.Count);
        }
        [Fact]
        public async Task AddUserToGroupAsync_ShouldThrowSuchAGroupDoesNotExistExceptionIfTheGoupDoesNotExist()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };

            var group = new Group { Id = 1, OwnerId = 1 };
            data.Users.Add(userOne);
            data.Users.Add(userTwo);

            data.Groups.Add(group);

            await data.SaveChangesAsync();

            var groupService = new GroupService(data);

            //Act and Assert
            await Assert.ThrowsAnyAsync<SuchAGroupDoesNotExistException>(() =>
            groupService.AddUserToGroupAsync(2, 2, 1));
        }
        [Fact]
        public async Task AddUserToGroupAsync_ShouldThrowUserIsNotTheOwnerOfTheGroupExceptionIfAnyoneButTheOwnerOfTheGroupTriesToAddNewUser()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };
            var userThree = new User { Id = 3 };

            var group = new Group { Id = 1, OwnerId = 1 };
            data.Users.Add(userOne);
            data.Users.Add(userTwo);
            data.Users.Add(userThree);

            data.Groups.Add(group);

            await data.SaveChangesAsync();

            var groupService = new GroupService(data);

            //Act and Assert
            await Assert.ThrowsAnyAsync<UserIsNotTheOwnerOfTheGroupException>(() =>
            groupService.AddUserToGroupAsync(1, 3, 2));
        }
        [Fact]
        public async Task AddUserToGroupAsync_ShouldThrowSuchAUserDoesNotExistExceptionIfTheUserThatIsBeingAddedDoesNoExist()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };

            var group = new Group { Id = 1, OwnerId = 1 };
            data.Users.Add(userOne);
            data.Users.Add(userTwo);

            data.Groups.Add(group);

            await data.SaveChangesAsync();

            var groupService = new GroupService(data);

            //Act and Assert
            await Assert.ThrowsAnyAsync<SuchAUserDoesNotExistException>(() =>
            groupService.AddUserToGroupAsync(1, 3, 1));
        }
        [Fact]
        public async Task AddUserToGroupAsync_ShouldThrowUserAlreadyPartOfTheGroupExceptionIfUserThatIsBeingAddedIsAlreadyParfOfTheGroup()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };

            var group = new Group { Id = 1, OwnerId = 1 };
            data.Users.Add(userOne);
            data.Users.Add(userTwo);

            data.Groups.Add(group);

            await data.SaveChangesAsync();

            var groupService = new GroupService(data);

            //Act

            await groupService.AddUserToGroupAsync(1, 2, 1);

            //Assert
            await Assert.ThrowsAnyAsync<UserAlreadyPartOfTheGroupException>(() =>
            groupService.AddUserToGroupAsync(1, 2, 1));
        }
        [Fact]
        public async Task DeleteUserFromGroupAsync_ShouldRemoveTheUserFromTheGroup()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };

            var group = new Group { Id = 1, OwnerId = 1 };
            data.Users.Add(userOne);
            data.Users.Add(userTwo);

            data.Groups.Add(group);

            await data.SaveChangesAsync();

            var groupService = new GroupService(data);

            //Act

            await groupService.AddUserToGroupAsync(1, 2, 1);
            var result = await groupService.ReturnAllUsersFromGroupAsync(1);
            Assert.Equal(1, result.Value.Count);
            await groupService.DeleteUserFromGroupAsync(1, 2, 1);
            result = await groupService.ReturnAllUsersFromGroupAsync(1);

            //Assert

            Assert.Equal(0, result.Value.Count);
        }
        [Fact]
        public async Task DeleteUserFromGroupAsync_ShouldThrowSuchAGroupDoesNotExistExceptionIfTheGroupDoesNotExist()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };

            var group = new Group { Id = 1, OwnerId = 1 };
            data.Users.Add(userOne);
            data.Users.Add(userTwo);

            data.Groups.Add(group);

            await data.SaveChangesAsync();

            var groupService = new GroupService(data);

            //Act and Assert

            await Assert.ThrowsAsync<SuchAGroupDoesNotExistException>(() => 
            groupService.DeleteUserFromGroupAsync(2, 2, 1));
        }
        [Fact]
        public async Task DeleteUserFromGroupAsync_ShouldThrowUserIsNotTheOwnerOfTheGroupExceptionIfTheUserTryingToRemoveAnotherUserIsNotTheOwnerOfTheGroup()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };

            var group = new Group { Id = 1, OwnerId = 1 };
            data.Users.Add(userOne);
            data.Users.Add(userTwo);

            data.Groups.Add(group);

            await data.SaveChangesAsync();

            var groupService = new GroupService(data);

            //Act and Assert

            await Assert.ThrowsAsync<UserIsNotTheOwnerOfTheGroupException>(() =>
            groupService.DeleteUserFromGroupAsync(1, 1, 2));
        }
        [Fact]
        public async Task DeleteUserFromGroupAsync_ShouldThrowOwnerCannotRemoveThemselvesFromTheGroupExceptionIfTheOwnerOfTheGroupTriesToRemoveThemselvesFromTheGroup()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };

            var group = new Group { Id = 1, OwnerId = 1 };
            data.Users.Add(userOne);
            data.Users.Add(userTwo);

            data.Groups.Add(group);

            await data.SaveChangesAsync();

            var groupService = new GroupService(data);

            //Act and Assert

            await Assert.ThrowsAsync<OwnerCannotRemoveThemselvesFromTheGroupException>(() =>
            groupService.DeleteUserFromGroupAsync(1, 1, 1));
        }
        [Fact]
        public async Task DeleteUserFromGroupAsync_ShouldThrowSuchAUserDoesNotExistExceptionIfSuchUserDoesNotExist()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };

            var group = new Group { Id = 1, OwnerId = 1 };
            data.Users.Add(userOne);
            data.Users.Add(userTwo);

            data.Groups.Add(group);

            await data.SaveChangesAsync();

            var groupService = new GroupService(data);

            //Act and Assert

            await Assert.ThrowsAsync<SuchAUserDoesNotExistException>(() =>
            groupService.DeleteUserFromGroupAsync(1, 3, 1));
        }
        [Fact]
        public async Task DeleteUserFromGroupAsync_ShouldThrowUserIsNotPartOfTheGroupExceptionIfTheRemovedUserIsNotPartOfTeGroup()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var userTwo = new User { Id = 2 };
            var userThree = new User { Id = 3 };


            var group = new Group { Id = 1, OwnerId = 1 };
            data.Users.Add(userOne);
            data.Users.Add(userTwo);
            data.Users.Add(userThree);

            data.Groups.Add(group);

            await data.SaveChangesAsync();

            var groupService = new GroupService(data);

            //Act and Assert

            await Assert.ThrowsAsync<UserIsNotPartOfTheGroupException>(() =>
            groupService.DeleteUserFromGroupAsync(1, 3, 1));
        }
    }
}
