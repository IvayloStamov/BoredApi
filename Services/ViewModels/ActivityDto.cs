using BoredApi.Data.DataModels.Enums;

namespace BoredApi.Services.ViewModels
{
    public class ActivityDto
    {
        public string ActivityName { get; set; } = string.Empty;
        public Status Status { get; set; }
    }
}
