using Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoredApi.Data.DataModels
{
    public class JoinActivityRequest
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }


        [ForeignKey("ActivityId, GroupId")]
        public virtual GroupActivity GroupActivity { get; set; }
        public int ActivityId { get; set; }
        public int GroupId { get; set; }

        public bool? HasAccepted { get; set; }
    }
}
