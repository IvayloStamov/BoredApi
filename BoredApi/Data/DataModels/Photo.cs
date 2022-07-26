using System.ComponentModel.DataAnnotations;

namespace BoredApi.Data.DataModels
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        [Required]
        public string PhotoText { get; set; } = string.Empty;
    }
}
