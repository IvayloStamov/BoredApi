using BoredApi.Data.Models.Enums;

namespace BoredApi.Dtos
{
    public class RequestDto
    {
        public string Name { get; set; } = string.Empty;
        public Status HasAccepted { get; set; }
    }
}
