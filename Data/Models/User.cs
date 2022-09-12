using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoredApi.Data.Models
{
    public class User
    {
        public User()
        {
            UserGroups = new List<UserGroup>();
            JoinActivityRequests = new List<JoinActivityRequest>();
        }
        [Key]
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public ICollection<UserGroup> UserGroups { get; set; }
        public ICollection<JoinActivityRequest> JoinActivityRequests { get; set; }
    }
}