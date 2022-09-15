using System;
using System.Linq;
using System.Threading.Tasks;
using BoredApi.Data;
using BoredApi.Data.Models;
using BoredApi.Data.Models.Enums;
using BoredApi.Services.BoredApi;
using BoredApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoredApi.Services
{
    public class ActivityService : IActivityService
    {
        private readonly BoredApiContext _boredApiContext;
        private readonly IActivityProvider _activityProvider;

        public ActivityService(BoredApiContext boredApiContext, IActivityProvider activityProvider)
        {
            _boredApiContext = boredApiContext;
            _activityProvider = activityProvider;
        }

        public async Task<ActionResult<string>> GetRandomActivityInGroupAsync(int userId, int groupId)
        {
            var group = await _boredApiContext.Groups
                            .Include(ug => ug.UserGroups)
                            .FirstOrDefaultAsync(x => x.Id == groupId)
                        ?? throw new ApplicationException($"SuchAGroupDoesNotExistException {groupId}");

            var user = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Id == userId)
                       ?? throw new ApplicationException($"SuchAUserDoesNotExistException {userId}");

            var groupActivities = await _boredApiContext.GroupActivities
                .Where(x => x.GroupId == groupId)
                .ToListAsync();

            // TODO: Test - accept activity then end it, then try to start a new one / the cancel endpint;
            bool active = groupActivities.Any(x => x.Status == Status.Pending ||
                                                   (x.Status == Status.Accepted && x.EndDate == null));
            if (active)
            {
                throw new ApplicationException($"ActivActivityException");
            }

            var randomActivity = await _activityProvider.GetRandomActivity(group.UserGroups.Count);
            
            var groupActivity = new GroupActivity()
            {
                GroupId = groupId,
                Activity = new Activity { Name = randomActivity },
                StartDate = DateTime.Now,
                Status = 0,
                Name = randomActivity
            };

            await _boredApiContext.GroupActivities.AddAsync(groupActivity);
            await _boredApiContext.SaveChangesAsync();

            var usersInCurrentGroup = await _boredApiContext.Users
                .Where(x => x.UserGroups.Any(x => x.GroupId == groupId))
                .ToListAsync();

            foreach (var userInGroup in usersInCurrentGroup)
            {
                userInGroup.JoinActivityRequests.Add(new JoinActivityRequest
                {
                    UserId = userInGroup.Id,
                    GroupActivityId = groupActivity.Id,
                    HasAccepted = 0,
                    Name = groupActivity.Name,
                });
            }

            await _boredApiContext.SaveChangesAsync();
            
            return randomActivity;
        }

        public async Task<ActionResult<string>> EndAnActivityAsync(int userId, int groupId)
        {
            var group = await _boredApiContext.Groups
                .Include(ug => ug.UserGroups)
                .FirstOrDefaultAsync(x => x.Id == groupId);
            if (group == null)
            {
                throw new ApplicationException($"SuchAGroupDoesNotExistException{groupId}");
            }

            var joinRequests = await _boredApiContext.JoinActivityRequests
                .Include(x => x.User)
                .ThenInclude(y => y.UserGroups.Where(z => z.GroupId == groupId))
                .Where(x => x.HasAccepted == Status.Pending)
                .ToListAsync();

            var user = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new ApplicationException($"SuchAUserDoesNotExistException{userId}");
            }

            GroupActivity? groupActivity = await _boredApiContext.GroupActivities
                .Where(x => x.GroupId == groupId && x.Status != Status.Declined && x.EndDate == null)
                .FirstOrDefaultAsync();

            string response = "";
            if (groupActivity == null)
            {
                response = "Currently there is no active activity.";
                return response;
            }

            if ((int) groupActivity.Status == 0)
            {
                groupActivity.Status = Status.Declined;
                response = "The activity has been cancelled.";

                joinRequests.ForEach(x => x.HasAccepted = Status.Declined);
            }
            else if (((int) groupActivity.Status == 1 && groupActivity.EndDate == null))
            {
                groupActivity.EndDate = DateTime.Now;
                response = "The activity has ended.";
            }

            await _boredApiContext.SaveChangesAsync();

            return response;
        }
    }
    // TODO: Implement Error handling middleware / BadRequest should be returned
}