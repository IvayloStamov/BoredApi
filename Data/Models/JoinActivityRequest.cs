using BoredApi.Data.DataModels.Enums;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoredApi.Data.Models
{
    public class JoinActivityRequest
    {
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; } = null!;

        [ForeignKey(nameof(GroupActivity))]
        public int GroupActivityId { get; set; }
        public GroupActivity GroupActivity { get; set; } = null!;
        public string Name { get; set; }
        public Status HasAccepted { get; set; }
    }
}
