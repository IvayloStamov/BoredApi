using BoredApi.Data.Models;

namespace BoredApi.Services.ViewModels
{
    public class GroupDto
    {
        public string Name { get; set; } = string.Empty;
        public List<int>? Users { get; set; } = new List<int>();
    }
}
