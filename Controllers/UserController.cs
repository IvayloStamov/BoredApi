using BoredApi.Data;
using BoredApi.Data.Models;
using BoredApi.Services;
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
        private readonly BoredApiContext _context;
        private readonly AddUserService _addUserService;

        public UserController(BoredApiContext context, AddUserService addUserService)
        {
            _context = context;
            _addUserService = addUserService;
        }
        [HttpGet]
        public async Task<ActionResult<List<User>>> Get()
        {
            var users = await _context.Users.ToListAsync();

            return Ok(users);
        }

        [HttpPost]
        public async Task<ActionResult<List<User>>> AddUser(UserDto user)
        {            
            return await _addUserService.AddUserToTheDatabase(user);
        }

        //  [HttpGet("{typeOfActivity}/{number}")]
        //  public async Task<ActionResult<UserReturnDto>> GetUsersBasedOnActivityType(string typeOfActivity, int number)
        //  {
        //      var Url = $"http://www.boredapi.com/api/activity?type={typeOfActivity}";
        //
        //      var httpClient = new HttpClient();
        //      var response = await httpClient.GetAsync(Url);
        //      response.EnsureSuccessStatusCode();            
        //
        //      var jsonString = await response.Content.ReadAsStringAsync();
        //
        //      BoredApiResponse? boredApi = Newtonsoft.Json.JsonConvert.DeserializeObject<BoredApiResponse>(jsonString);
        //
        //      List<User> listOfUsers = await context.UserActivities
        //          .Include(x => x.User)
        //          .Where(y => y.Activity.ActivityName == typeOfActivity)
        //          .Select(y => y.User)
        //          .Take(number)
        //          .ToListAsync();
        //
        //      if(listOfUsers.Count == 0)
        //      {
        //          return BadRequest($"No users want to take part in this type of activity - {typeOfActivity}");
        //      }
        //
        //      List<string> listOfUsernames = new List<string>();
        //
        //      foreach (var user in listOfUsers)
        //      {
        //          listOfUsernames.Add(user.Username);
        //      }
        //
        //      var userReturnDto = new UserReturnDto
        //      {
        //          Activity = boredApi.Activity,
        //          TypeOfActivity = typeOfActivity,
        //          ListOfUsernames = listOfUsernames
        //      };
        //
        //      return userReturnDto;
        //  }
    }
}
