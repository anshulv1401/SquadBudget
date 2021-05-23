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
        public int InstallmentDayOfMonth { get; set; }
        public double DefaultLoanInterest { get; set; }
        public int DefaultNoOfInstallment { get; set; }
        public double BankInstallmentDelayFine { get; set; }
        public int BankInstallmentDelayFineType { get; set; }
        public int BankInstallmentDelayFineTerm { get; set; }
        public double LoanDelayFine { get; set; }
        public int LoanDelayFineType { get; set; }
        public int LoanDelayFineTerm { get; set; }
        public int InterestTermID { get; set; }
        public string DateFormat { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
