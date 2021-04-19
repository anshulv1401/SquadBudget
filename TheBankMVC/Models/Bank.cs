using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBankMVC.Models
{
    public class Bank
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public double BankInstallmentAmount { get; set; }
        public double DefaultLoanInterest { get; set; }
        public int DefaultNoOfInstallment { get; set; }
        public double BankInstallmentDelayFine { get; set; }
        public int BankInstallmentDelayFineType { get; set; }
        public int BankInstallmentDelayFinePeriod { get; set; }
        public double LoanDelayFine { get; set; }
        public int LoanDelayFineType { get; set; }
        public int LoanDelayFinePeriod { get; set; }
        public int InterestTermID { get; set; }
        public string DateFormat { get; set; }
    }
}
