using BoredApi.Data.Models.Enums;

namespace BoredApi.Dtos
{
    public class ActivityDto
    {
        public string ActivityName { get; set; } = string.Empty;
        public Status Status { get; set; }
    }
}
