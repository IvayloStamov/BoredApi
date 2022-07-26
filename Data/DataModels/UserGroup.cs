using Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace BoredApi.Data.DataModels
{
    public class UserGroup
    {
        [ForeignKey(nameof(Group))]
        public int GroupId { get; set; }
        public virtual Group Group { get; set; }

        [ForeignKey(nameof(User))]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public string Username { get; set; } = string.Empty;
        public DateTime UserEntryDate { get; set; }
    }
}
