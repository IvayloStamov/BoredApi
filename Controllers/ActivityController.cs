using System.Threading.Tasks;
using BoredApi.Services;
using BoredApi.Services.BoredApi;
using BoredApi.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace BoredApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ActivityController : ControllerBase
    {
        private readonly IActivityService _activityService;
        private readonly IActivityProvider _activityProvider;

        public ActivityController(IActivityService activityService, IActivityProvider activityProvider)
        {
            _activityService = activityService;
            _activityProvider = activityProvider;
        }
        //GET https://localhost:5000/api/activity?userId=4&groupId=2
        //GET https://localhost:5000/api/activity
        //GET https://localhost:5000/api/activity/random/{broqHora}

        public record RandomActivityModel(int UserId, int GroupId);
        
        [HttpGet("random/{numberOfPeople}")]
        public async Task<ActionResult<string>> GetRandom(int numberOfPeople)
        {
            var randomActivity = await _activityProvider.GetRandomActivity(numberOfPeople);
            return randomActivity;
        }

        [HttpGet]
        public async Task<ActionResult<string>> GetRandomActivityInGroup([FromQuery] RandomActivityModel model)
        {
            return await _activityService.GetRandomActivityInGroupAsync(model.UserId, model.GroupId);
        }

        [HttpPost]
        public async Task<ActionResult<string>> EndActivity(int userId, int groupId)
        {
            return await _activityService.EndAnActivityAsync(userId, groupId);
        }
    }
}