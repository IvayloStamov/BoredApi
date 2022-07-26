using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoredApi.Data.DataModels
{
    public class Photo
    {
        [Key]
        public int PhotoId { get; set; }
        [Required]
        public string PhotoText { get; set; } = string.Empty;

        [ForeignKey("GroupId, ActivityId")]
        public GroupActivity GroupActivity { get; set; }
        public int GroupId { get; set; }
        public int ActivityId { get; set; }
    }
}
