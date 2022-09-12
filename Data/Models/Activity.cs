using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BoredApi.Data.Models
{
    public class Activity
    {      
        public int Id { get; set; }
        [Required]
        public string Name { get; set; } = string.Empty;
        public virtual ICollection<GroupActivity>? GroupActivities { get; set; } = new List<GroupActivity>();
    }
}