﻿using BoredApi.Data;
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
        private static readonly HttpClient HttpClient = new HttpClient();
        public ActivityService(BoredApiContext boredApiContext)
        {
            _boredApiContext = boredApiContext;
        }

        public async Task<ActionResult<string>> GetRandomActivityAloneAsync()
        {
            var Url = $"http://www.boredapi.com/api/activity?participants={1}";

            var response = await HttpClient.GetAsync(Url);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            BoredApiResponse? boredApiResponse = JsonConvert.DeserializeObject<BoredApiResponse>(jsonString);
            string activityName = boredApiResponse.Activity;

            Activity activity = new Activity()
            {
                Name = activityName,
            };

            return activityName;
        }

        public async Task<ActionResult<string>> GetRandomActivityInGroupAsync(int userId, int groupId)
        {
            var group = await _boredApiContext.Groups
                .Include(ug => ug.UserGroups)
                .FirstOrDefaultAsync(x => x.Id == groupId);
            if (group == null)
            {
                // TODO: Use proper exceptions
                throw new KeyNotFoundException($"A group with the id ({groupId}) does not exist.");
            }

            var user = await _boredApiContext.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null)
            {
                throw new Exception($"A user with the id ({userId}) does not exist.");
            }

            // TODO: Refactor the code, extract it to another service
            int numberOfPeople = group.UserGroups.Count;
            var Url = $"http://www.boredapi.com/api/activity?participants={numberOfPeople}";

            var response = await HttpClient.GetAsync(Url);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            BoredApiResponse? boredApiResponse = JsonConvert.DeserializeObject<BoredApiResponse>(jsonString);
            string activityName = boredApiResponse.Activity;

            Activity activity = new Activity()
            {
                Name = activityName,
            };

            var groupActivities = await _boredApiContext.GroupActivities
                .Where(x => x.GroupId == groupId)
                .ToListAsync();

            foreach (var ga in groupActivities)
            {
                if (ga.Status == Status.Pending || (ga.Status == Status.Accepted && ga.EndDate == null))
                {
                    throw new Exception("There is already an active activity.");
                }
            }

            _boredApiContext.Activities.Add(activity);
            await _boredApiContext.SaveChangesAsync();

            GroupActivity groupActivity = new GroupActivity()
            {
                GroupId = groupId,
                ActivityId = activity.Id,
                StartDate = DateTime.Now,
                Status = 0,
                Name = activity.Name
            };

            _boredApiContext.GroupActivities.Add(groupActivity);
            await _boredApiContext.SaveChangesAsync();


            // TODO: This needs to be remade and no SaveChanges in forloops
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

        // TODO: Implement Error handling middleware / BadRequest should be returned
        public async Task<ActionResult<string>> EndAnActivityAsync(int userId, int groupId)
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
            var groupActivities = await _boredApiContext.GroupActivities
                .Where(x => x.GroupId == groupId)
                .ToListAsync();

            string response = "";

            foreach (var ga in groupActivities)
            {
                if ((int)ga.Status == 0 )
                {
                    ga.Status = Status.Declined;
                    response = "The activity has been cancelled";
                }
                else if(((int)ga.Status == 1 && ga.EndDate == null))
                {
                    ga.EndDate = DateTime.Now;
                    response = "The activity has ended";
                }
            }
            await _boredApiContext.SaveChangesAsync();

            return response;
        }
    }
}