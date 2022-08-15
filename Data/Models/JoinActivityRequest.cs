using System.ComponentModel.DataAnnotations.Schema;

namespace BoredApi.Data.Models
{
    public class JoinActivityRequest
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;


        [ForeignKey("ActivityId, GroupId")]
        public GroupActivity GroupActivity { get; set; } = null!;
        public int ActivityId { get; set; }
        public int GroupId { get; set; }

        public bool? HasAccepted { get; set; }
    }
}
