using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBankMVC.Models
{
    public class EMIHeader
    {
        public int EMIHeaderId { get; set; }
        public double EMIAmount { get; set; }
        public double LoanAmount { get; set; }
        public double MonthlyRateOfInterest { get; set; }
        public int NoOfInstallment { get; set; }
        public int LockInPeriod { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public string DateFormat { get; set; }
        public IEnumerable<Installment> Installments { get; set; }
    }
}
