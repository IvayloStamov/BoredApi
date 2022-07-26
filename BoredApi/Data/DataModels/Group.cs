using System.ComponentModel.DataAnnotations;

namespace BoredApi.Data.DataModels
{
    public class Group
    {
        public Group()
        {
            UserGroups = new List<UserGroup>();
           // PrefferedActivities = new List<string>();
            GroupActivities = new List<GroupActivity>();
        }
        [Key]
        public int GroupId { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTime CreateDate { get; set; }
       // public virtual ICollection<string> PrefferedActivities { get; set; }
        public virtual ICollection<UserGroup>? UserGroups { get; set; }
        public virtual ICollection<GroupActivity>? GroupActivities { get; set; }
    }
}
