using BoredApi.Data.DataModels.Enums;

namespace BoredApi.Services.ViewModels
{
    public class RequestDto
    {
        public string Name { get; set; } = string.Empty;
        public Status HasAccepted { get; set; }
    }
}
