using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Activity
    {
        public Activity()
        {
            UserActivities = new List<UserActivity>();
        }
        public int ActivityId { get; set; }
        public string ActivityName { get; set; } = string.Empty;
        public ICollection<UserActivity> UserActivities { get; set; }
    }
}
