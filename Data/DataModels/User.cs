using BoredApi.Data.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class User
    {
        public User()
        {
            UserGroups = new List<UserGroup>();
            JoinActivityRequests = new List<JoinActivityRequest>();
        }
        [Key]
        public int UserId { get; set; }
        public string Username { get; set; } = string.Empty;
        public string FirstName { get; set; } = string.Empty;
        public string LastName { get; set; } = string.Empty;
        public virtual ICollection<UserGroup>? UserGroups { get; set; }
        public virtual ICollection<JoinActivityRequest> JoinActivityRequests { get; set; }
    }
}