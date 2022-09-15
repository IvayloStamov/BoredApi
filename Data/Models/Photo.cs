using System.ComponentModel.DataAnnotations;

namespace BoredApi.Data.Models
{
    public class Photo
    {
        public int Id { get; set; }
        [Required]
        public string PhotoText { get; set; } = string.Empty;

        public int GroupId { get; set; }
        public int ActivityId { get; set; }
        public GroupActivity GroupActivity { get; set; } = null!;
    }
}
