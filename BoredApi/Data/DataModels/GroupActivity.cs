using BoredApi.Data.DataModels.Enums;
using Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoredApi.Data.DataModels
{
    public class GroupActivity
    {
        public GroupActivity()
        {
            Photos = new List<Photo>();
            JoinActivityRequests = new List<JoinActivityRequest>();
        }
        [ForeignKey(nameof(Group))]
        public int GroupId { get; set; }
        public Group Group { get; set; }

        [ForeignKey(nameof(Activity))]
        public int ActivityId { get; set; }
        public Activity Activity { get; set; }

        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }

        public Status Status { get; set; } 
        public virtual ICollection<Photo> Photos { get; set; }
        public virtual ICollection<JoinActivityRequest> JoinActivityRequests { get; set; }

    }
}
