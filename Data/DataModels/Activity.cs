using BoredApi.Data.DataModels;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class Activity
    {
        public Activity()
        {
            GroupActivities = new List<GroupActivity>();
        }
        [Key]
        public int ActivityId { get; set; }
        [Required]
        public string ActivityName { get; set; } = string.Empty;
        public virtual ICollection<GroupActivity>? GroupActivities { get; set; }

    }
}