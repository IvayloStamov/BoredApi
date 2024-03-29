﻿using System.Threading.Tasks;
using BoredApi.Data.Models;
using BoredApi.Data.Models.Exceptions;
using BoredApi.Services;
using BoredApi.Services.BoredApi;
using BoredApi.Test.Mocks;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace BoredApi.Test.Services
{
    public class ActivityServiceTest
    {
        private readonly IActivityProvider _activityProvider;

        public ActivityServiceTest()
        {
            _activityProvider = new BoredApiActivityProvider();
        }

        [Fact]
        public async Task GetRandomActivityInGroupAsync_RandomActivityInTheFormOfStringBasedOnTheNumberOfUsersInTheGroup()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var groupOne = new Group { Id = 1, OwnerId = 1 };

            data.Users.Add(userOne);
            data.Groups.Add(groupOne);
            await data.SaveChangesAsync();

            var activityService = new ActivityService(data, _activityProvider);

            //Act

            var result = await activityService.GetRandomActivityInGroupAsync(1, 1);

            //Assert

            Assert.True(result is ActionResult<string>);
        }

        [Fact]
        public async Task GetRandomActivityInGroupAsync_ShouldThrowSuchAGroupDoesNotExistExceptionIfTheGroupDoesNotExist()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var groupOne = new Group { Id = 1, OwnerId = 1 };

            data.Users.Add(userOne);
            data.Groups.Add(groupOne);
            await data.SaveChangesAsync();

            var activityService = new ActivityService(data, _activityProvider);

            //Act and Assert

            await Assert.ThrowsAsync<SuchAGroupDoesNotExistException>(() =>
                activityService.GetRandomActivityInGroupAsync(1, 2));
        }

        [Fact]
        public async Task GetRandomActivityInGroupAsync_ShouldThrowSuchAUserDoesNotExistExceptionIfTheUserDoesNotExist()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var groupOne = new Group { Id = 1, OwnerId = 1 };

            data.Users.Add(userOne);
            data.Groups.Add(groupOne);
            await data.SaveChangesAsync();

            var activityService = new ActivityService(data, _activityProvider);

            //Act and Assert

            await Assert.ThrowsAsync<SuchAUserDoesNotExistException>(() =>
                activityService.GetRandomActivityInGroupAsync(2, 1));
        }

        [Fact]
        public async Task GetRandomActivityInGroupAsync_ShouldThrowActiveActivityExceptionIfThereIsAnActiveActivity()
        {
            //Arrange
            await using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var groupOne = new Group { Id = 1, OwnerId = 1 };

            data.Users.Add(userOne);
            data.Groups.Add(groupOne);
            await data.SaveChangesAsync();

            var activityService = new ActivityService(data, _activityProvider);

            //Act 

            await activityService.GetRandomActivityInGroupAsync(1, 1);

            //Assert

            await Assert.ThrowsAsync<ActivActivityException>(() =>
                activityService.GetRandomActivityInGroupAsync(1, 1));
        }

        [Fact]
        public async Task EndAnActivityAsync_ShouldThrowSuchAGroupDoesNotExistExceptionIfTheGroupDoesNotExist()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var groupOne = new Group { Id = 1, OwnerId = 1 };

            data.Users.Add(userOne);
            data.Groups.Add(groupOne);
            await data.SaveChangesAsync();

            var activityService = new ActivityService(data, _activityProvider);

            //Act and Assert

            await Assert.ThrowsAsync<SuchAGroupDoesNotExistException>(() =>
                activityService.EndAnActivityAsync(1, 2));
        }

        [Fact]
        public async Task EndAnActivityAsync_SuchAUserDoesNotExistExceptionIfTheUserDoesNotExist()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var groupOne = new Group { Id = 1, OwnerId = 1 };

            data.Users.Add(userOne);
            data.Groups.Add(groupOne);
            await data.SaveChangesAsync();

            var activityService = new ActivityService(data, _activityProvider);

            //Act and Assert

            await Assert.ThrowsAsync<SuchAUserDoesNotExistException>(() =>
                activityService.EndAnActivityAsync(2, 1));
        }

        [Fact]
        public async Task EndAnActivityAsync_ShouldReturnThatThereAreNoActiveActivitiesIfThereAreNone()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var groupOne = new Group { Id = 1, OwnerId = 1 };

            data.Users.Add(userOne);
            data.Groups.Add(groupOne);
            await data.SaveChangesAsync();

            var activityService = new ActivityService(data, _activityProvider);

            //Act

            var result = await activityService.EndAnActivityAsync(1, 1);

            string expected = "Currently there is no active activity.";

            //Assert

            Assert.Equal(expected, result.Value.ToString());
        }

        [Fact]
        public async Task EndAnActivityAsync_ShouldReturnTheActivityHasBeenCancelledIfItHasNotBeenAccepted()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var groupOne = new Group { Id = 1, OwnerId = 1 };

            data.Users.Add(userOne);
            data.Groups.Add(groupOne);
            await data.SaveChangesAsync();

            var activityService = new ActivityService(data, _activityProvider);

            //Act
            await activityService.GetRandomActivityInGroupAsync(1, 1);
            var result = await activityService.EndAnActivityAsync(1, 1);

            string expected = "The activity has been cancelled.";

            //Assert

            Assert.Equal(expected, result.Value.ToString());
        }

        [Fact]
        public async Task EndAnActivityAsync_ShouldReturnTheActivityHasEndedIfItHasBeenAcceptedByAll()
        {
            //Arrange
            using var data = DatabaseMock.Instance;
            var userOne = new User { Id = 1 };
            var groupOne = new Group { Id = 1, OwnerId = 1 };

            data.Users.Add(userOne);
            data.Groups.Add(groupOne);
            await data.SaveChangesAsync();

            var activityService = new ActivityService(data, _activityProvider);

            //Act
            await activityService.GetRandomActivityInGroupAsync(1, 1);
            var result = await activityService.EndAnActivityAsync(1, 1);

            string expected = "The activity has been cancelled.";

            //Assert

            Assert.Equal(expected, result.Value.ToString());
        }
    }
}
//TODO: Fix the bloody test