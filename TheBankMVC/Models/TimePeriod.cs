using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBankMVC.Models
{
    public class TimePeriod
    {
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DateFormat { get; set; }
    }
}
