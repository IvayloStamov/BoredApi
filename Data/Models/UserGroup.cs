using System;

namespace BoredApi.Data.Models
{
    public class UserGroup
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }
        public int UserId { get; set; }
        public User User { get; set; } = null!;
        public bool IsAdmin { get; set; } = false;
        public bool IsOwner { get; set; } = false;
        public DateTime UserEntryDate { get; set; }
    }
}
