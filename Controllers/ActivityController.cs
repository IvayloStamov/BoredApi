using BoredApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoredApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        public ActivityController(IActivityService activityService)
        {
            _activityService = activityService;
        }
        [HttpGet]
        public async Task<ActionResult<string>> GetRandomActivity(int userId, int groupId)
        {
            return await _activityService.GetRandomActivityAsync(userId, groupId);
        }
    }
}
