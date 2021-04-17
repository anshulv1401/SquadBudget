using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBankMVC.Models
{
    public class EMIHeader
    {
        public int Id { get; set; }
        public double MonthlyRateOfInterest { get; set; }
        public double EMIAmount { get; set; }
    }
}
