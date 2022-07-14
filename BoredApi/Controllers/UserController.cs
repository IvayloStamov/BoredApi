using Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
        public async Task<IActionResult> Get()
        {
            var users = new List<User>
            {
                new User{UserId = 1, FirstName = "Ivaylo", LastName = "Stamov", Username = "eisen",
                Activities = new List<string> { 
                    "edication", "social", "diy"
                } },
            };
            return Ok(users);
        }


    }
}
