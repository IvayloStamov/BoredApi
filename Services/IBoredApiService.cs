using BoredApi.Data.Models;

namespace BoredApi.Services
{
    public interface IBoredApiService
    {
        public Task<Activity> CallBoredApiAsync(int numberOfPeople);
    }
}
