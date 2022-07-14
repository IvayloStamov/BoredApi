using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserActivity
    {
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }
    }
}