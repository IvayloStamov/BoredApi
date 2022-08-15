using Microsoft.AspNetCore.Mvc;

namespace BoredApi.Services
{
    public interface IActivityService
    {
        public Task<ActionResult<string>> GetRandomActivityAsync(int userId, int groupId);
    }
}
