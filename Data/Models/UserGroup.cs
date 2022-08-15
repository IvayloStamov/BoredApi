using System.ComponentModel.DataAnnotations.Schema;

namespace BoredApi.Data.Models
{
    public class UserGroup
    {
        [ForeignKey(nameof(Group))]
        public int GroupId { get; set; }
        public Group Group { get; set; } = null!;
        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public bool isAdmin { get; set; } = false;
        public bool isOwner { get; set; } = false;
        public DateTime UserEntryDate { get; set; }
    }
}
