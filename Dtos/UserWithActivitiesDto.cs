using System.Collections.Generic;

namespace BoredApi.Dtos
{
    public class UserWithActivitiesDto
    {
        public string Username { get; set; } = string.Empty;
        public List<ActivityDto> Requests { get; set; } = new List<ActivityDto>();
    }
}
