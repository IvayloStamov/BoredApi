namespace BoredApi.Data.Models
{
    public class UserGroup
    {
        public int GroupId { get; set; }
        public Group Group { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public string Username { get; set; } = string.Empty;
        public DateTime UserEntryDate { get; set; }
    }
}
