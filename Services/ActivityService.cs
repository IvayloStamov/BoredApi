using BoredApi.Data;
using BoredApi.Data.DataModels.Enums;
using BoredApi.Data.Models;
using BoredApi.Data.Models.Exceptions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BoredApi.Services
{
    public class ActivityService : IActivityService
    {
        private readonly BoredApiContext _boredApiContext;
        private readonly IBoredApiService _boredApiService;
        public ActivityService(BoredApiContext boredApiContext, IBoredApiService boredApiService)
        {
            _boredApiContext = boredApiContext;
            _boredApiService = boredApiService;
        }

        public async Task<ActionResult<string>> GetRandomActivityAloneAsync()
        {
            var result = await _boredApiService.CallBoredApiAsync(1);
            return result.Name;
        }
        public async Task<ActionResult<string>> GetRandomActivityInGroupAsync(int userId, int groupId)
        {
            var group = await _boredApiContext.Groups
                .Include(ug => ug.UserGroups)
                .FirstOrDefaultAsync(x => x.Id == groupId);
            if (group == null)
            {
                // TODO: Use proper exceptions
                throw new SuchAGroupDoesNotExistException(groupId);
            }

            var user = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new SuchAUserDoesNotExistException(userId);
            }


            var groupActivities = await _boredApiContext.GroupActivities
                .Where(x => x.GroupId == groupId)
                .ToListAsync();
            // TODO: Test - accept activity then end it, then try to start a new one / the cancel endpint;
            bool active = groupActivities.Any(x => x.Status == Status.Pending ||
            (x.Status == Status.Accepted && x.EndDate == null));
            if (active)
            {
                throw new ActivActivityException();
            }

            int numberOfPeople = group.UserGroups.Count;

            Activity activity = await _boredApiService.CallBoredApiAsync(numberOfPeople);

            await _boredApiContext.Activities.AddAsync(activity);
            await _boredApiContext.SaveChangesAsync();

            GroupActivity groupActivity = new GroupActivity()
            {
                GroupId = groupId,
                ActivityId = activity.Id,
                StartDate = DateTime.Now,
                Status = 0,
                Name = activity.Name
            };

            await _boredApiContext.GroupActivities.AddAsync(groupActivity);
            await _boredApiContext.SaveChangesAsync();

            // TODO: This needs to be remade and no SaveChanges in forloops


            //------------------------------------------------------------------------
            var usersInCurrentGroup = await _boredApiContext.Users
                .Where(x => x.UserGroups.Any(x => x.GroupId == groupId))
                .ToListAsync();

            usersInCurrentGroup.ForEach(x => x.JoinActivityRequests.Add(new JoinActivityRequest
            {
                UserId = x.Id,
                GroupActivityId = groupActivity.Id,
                HasAccepted = 0,
                Name = groupActivity.Name,
            }));
            //------------------------------------------------------------------------

            await _boredApiContext.SaveChangesAsync();

            return activity.Name;
        }
        public async Task<ActionResult<string>> EndAnActivityAsync(int userId, int groupId)
        {
            var group = await _boredApiContext.Groups
                .Include(ug => ug.UserGroups)
                .FirstOrDefaultAsync(x => x.Id == groupId);
            if (group == null)
            {
                throw new SuchAGroupDoesNotExistException(groupId);
            }

            var joinRequests = await _boredApiContext.JoinActivityRequests
                .Include(x => x.User)
                .ThenInclude(y => y.UserGroups.Where(z => z.GroupId == groupId))   
                .Where(x => x.HasAccepted == Status.Pending)
                .ToListAsync();

            var user = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new SuchAUserDoesNotExistException(userId);
            }

            GroupActivity? groupActivity = await _boredApiContext.GroupActivities
                .Where(x => x.GroupId == groupId && x.Status != Status.Declined && x.EndDate == null)
                .FirstOrDefaultAsync();

            string response = "";
            if(groupActivity == null)
            {
                response = "Currently there is no active activity.";
                return response;
            }

            if ((int)groupActivity.Status == 0)
            {
                groupActivity.Status = Status.Declined;
                response = "The activity has been cancelled.";

                joinRequests.ForEach(x => x.HasAccepted = Status.Declined);
            }
            else if (((int)groupActivity.Status == 1 && groupActivity.EndDate == null))
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
