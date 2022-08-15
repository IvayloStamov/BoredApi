using BoredApi.Data;
using BoredApi.Data.DataModels.Enums;
using BoredApi.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;
using Newtonsoft.Json;

namespace BoredApi.Services
{
    public class ActivityService : IActivityService
    {
        private readonly BoredApiContext _boredApiContext;

        public ActivityService(BoredApiContext boredApiContext)
        {
            _boredApiContext = boredApiContext;
        }
        public async Task<ActionResult<string>> GetRandomActivityAsync(int userId, int groupId)
        {
            var group = await _boredApiContext.Groups
                .Include(ug => ug.UserGroups)
                .FirstOrDefaultAsync(x => x.Id == groupId);
            if (group == null)
            {
                throw new Exception($"A group with the id ({groupId}) does not exist.");
            }            

            var user = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception($"A user with the id ({userId}) does not exist.");
            }

            

            int numberOfPeople = group.UserGroups.Count;
            var Url = $"http://www.boredapi.com/api/activity?participants={numberOfPeople}";

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(Url);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            BoredApiResponse? boredApiResponse = JsonConvert.DeserializeObject<BoredApiResponse>(jsonString);
            string activityName = boredApiResponse.Activity;

            Activity activity = new Activity()
            {
                ActivityName = activityName,
            };

            _boredApiContext.Activities.Add(activity);
            await _boredApiContext.SaveChangesAsync();

            GroupActivity groupActivity = new GroupActivity()
            {
                GroupId = groupId,
                ActivityId = activity.Id,
                StartDate = DateTime.Now,
                Status = 0,
                Name = activity.ActivityName
            };

            _boredApiContext.GroupActivities.Add(groupActivity);
            await _boredApiContext.SaveChangesAsync();

            foreach (UserGroup u in group.UserGroups)
            {
                User currentUser = _boredApiContext.Users
                    .Include(ug => ug.UserGroups)
                    .First(x => x.Id == u.UserId);

                JoinActivityRequest joinRequest = new JoinActivityRequest()
                {
                    UserId = currentUser.Id,
                    GroupActivityId = groupActivity.Id,
                    HasAccepted = 0,
                    Name = groupActivity.Name,
                };

                currentUser.JoinActivityRequests.Add(joinRequest);
                await _boredApiContext.SaveChangesAsync();
            }
            

            return activityName;
        }
    }
}
