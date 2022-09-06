using BoredApi.Data.Models;
using Models;
using Newtonsoft.Json;

namespace BoredApi.Services
{
    public class BoredApiService : IBoredApiService
    {
        private static readonly HttpClient HttpClient = new HttpClient();
        public async Task<Activity> CallBoredApiAsync(int numberOfPeople)
        {
            
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

            return activity;
        }
    }
}
