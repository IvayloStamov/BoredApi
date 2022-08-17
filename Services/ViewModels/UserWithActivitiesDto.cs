using BoredApi.Data.Models;

namespace BoredApi.Services.ViewModels
{
    public class UserWithActivitiesDto
    {
        public string Username { get; set; } = string.Empty;
        public List<ActivityDto> Requests { get; set; } = new List<ActivityDto>();
    }
}
