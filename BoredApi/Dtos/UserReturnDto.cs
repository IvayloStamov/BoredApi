using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class UserReturnDto
    {
        public UserReturnDto()
        {
            List<UserReturnDto> list = new List<UserReturnDto>(); 
        }
        public string Activity { get; set; } = string.Empty;
        public string TypeOfActivity { get; set; } = string.Empty;
        public List<string> ListOfUsernames { get; set; }
    }
}
