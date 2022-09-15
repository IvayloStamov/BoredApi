using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace BoredApi.Services.BoredApi
{
    public interface IActivityProvider
    {
        public Task<string> GetRandomActivity(int numberOfPeople);
    }

    public class BoredApiActivityProvider : IActivityProvider
    {
        private static readonly HttpClient HttpClient = new HttpClient();

        public async Task<string> GetRandomActivity(int numberOfPeople)
        {
            var url = $"http://www.boredapi.com/api/activity?participants={numberOfPeople}";

            var response = await HttpClient.GetAsync(url);
            response.EnsureSuccessStatusCode();

            var jsonString = await response.Content.ReadAsStringAsync();

            var boredApiResponse = JsonConvert.DeserializeObject<BoredApiResponse>(jsonString);
            string activityName = boredApiResponse?.Activity;
            return activityName;
        }
    }
}