using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Models;

namespace BoredApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly DataContext context;
        public UserController(DataContext _context)
        {
            context = _context;            
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            var users = await context.Users.ToListAsync();

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(UserDto user)
        {
            string username = user.Username;

            var userCheck = context.Users.FirstOrDefault(x => x.Username == username);
            if (userCheck != null)
            {
                return BadRequest($"A user with the userhame ({username}) already exists.");
            }

            User newUser = new User
            {
                Username = user.Username,
                FirstName = user.FirstName,
                LastName = user.LastName
            };

            List<string> inputActivities = user.Activities.ToList();
            List<int> activityId = new List<int>();
            int counter = 0;
            foreach (var a in inputActivities)
            {
                bool isValidActivity = false;
                var activities = context.Activities.ToList();
                foreach (var item in activities)
                {
                    if (item.ActivityName.Equals(a))
                    {
                        activityId.Add(item.ActivityId);
                        isValidActivity = true;
                        counter++;
                        break;
                    }
                }
                if (!isValidActivity)
                {
                    return BadRequest($"Invalid activity ({a}). Please, remove or change it!");
                }

            }
            await context.AddAsync(newUser);
            await context.SaveChangesAsync();

            foreach (var aId in activityId)
            {
                var userActivity = new UserActivity
                {
                    UserId = newUser.UserId,
                    ActivityId = aId
                };
                await context.UserActivities.AddAsync(userActivity);
                await context.SaveChangesAsync();
            }
            return Ok(await context.Users.ToListAsync());
        }

        [HttpGet("{typeOfActivity}/{number}")]
        public async Task<ActionResult<UserReturnDto>> GetUsersBasedOnActivityType(string typeOfActivity, int number)
        {
            var Url = $"http://www.boredapi.com/api/activity?type={typeOfActivity}";

            var httpClient = new HttpClient();
            var response = await httpClient.GetAsync(Url);
            response.EnsureSuccessStatusCode();            

            var jsonString = await response.Content.ReadAsStringAsync();

            BoredApiResponse? boredApi = Newtonsoft.Json.JsonConvert.DeserializeObject<BoredApiResponse>(jsonString);

            List<User> listOfUsers = await context.UserActivities
                .Include(x => x.User)
                .Where(y => y.Activity.ActivityName == typeOfActivity)
                .Select(y => y.User)
                .Take(number)
                .ToListAsync();

            if(listOfUsers.Count == 0)
            {
                return BadRequest($"No users want to take part in this type of activity - {typeOfActivity}");
            }

            List<string> listOfUsernames = new List<string>();

            foreach (var user in listOfUsers)
            {
                listOfUsernames.Add(user.Username);
            }

            var userReturnDto = new UserReturnDto
            {
                Activity = boredApi.Activity,
                TypeOfActivity = typeOfActivity,
                ListOfUsernames = listOfUsernames
            };

            return userReturnDto;
        }
    }
}
