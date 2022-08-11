using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoredApi.Data.Models
{
    public class Group
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(250)]
        public string Name { get; set; } = string.Empty;
        [Required]
        public DateTime CreateDate { get; set; }
        public List<UserGroup> UserGroups { get; set; } = new List<UserGroup>();
        public ICollection<GroupActivity>? GroupActivities { get; set; } = new List<GroupActivity>();
    }
}
