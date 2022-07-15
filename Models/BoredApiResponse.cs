using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class BoredApiResponse
    {
        public string Activity { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Participants { get; set; }
        public decimal Price { get; set; }
        public string Link { get; set; } = string.Empty;
        public string Key { get; set; } = string.Empty;
        public double Accessibility { get; set; }
    }
}
