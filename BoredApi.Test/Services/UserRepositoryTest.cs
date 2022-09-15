using System.Threading.Tasks;
using BoredApi.Data.Models;
using BoredApi.Data.Models.Exceptions;
using BoredApi.Dtos;
using BoredApi.Services;
using BoredApi.Test.Mocks;
using Xunit;

namespace BoredApi.Test.Services
{
    public class UserRepositoryTest
    {
        [Fact]
        public async Task GetAllUsersAsync_ShouldReturnAllUsers()
        {
            //Arrange
            using var data = DatabaseMock.Instance;

            var userOne = new User { Id = 1, FirstName = "1", LastName = "1", Username = "1" };
            var userTwo = new User { Id = 2, FirstName = "2", LastName = "2", Username = "2" };
            data.Users.Add(userOne);
            data.Users.Add(userTwo);
            await data.SaveChangesAsync();
            var userRepository = new UserRepository(data);

            //Act

            var result = await userRepository.GetAllUsersAsync();

            //Assert

            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task AddUserToTheDatabaseAsync_ShouldAddNewUser()
        {
            //Arrange
            using var data = DatabaseMock.Instance;

            var userOne = new User { Id = 1, FirstName = "1", LastName = "1", Username = "1" };
            data.Users.Add(userOne);
            await data.SaveChangesAsync();
            var userRepository = new UserRepository(data);

            //Act
            var userTwo = new UserDto { FirstName = "2", LastName = "2", Username = "2" };

            await userRepository.AddUserToTheDatabaseAsync(userTwo);
            var result = await userRepository.GetAllUsersAsync();

            //Assert
            Assert.Equal(2, result.Count);
        }
        [Fact]
        public async Task AddUserToTheDatabaseAsync_ShouldThrowUserAlreadyExistsExceptionWhenAddingUserWithTheSameUsername()
        {
            //Arrange
            using var data = DatabaseMock.Instance;

            var userOne = new User { Id = 1, FirstName = "1", LastName = "1", Username = "1" };
            data.Users.Add(userOne);
            await data.SaveChangesAsync();
            var userRepository = new UserRepository(data);

            //Act
            var userTwo = new UserDto { FirstName = "2", LastName = "2", Username = "1" };

            //Assert
            await Assert.ThrowsAnyAsync<UserAlreadyExistsException>(() => userRepository.AddUserToTheDatabaseAsync(userTwo));
        }
    }
}
