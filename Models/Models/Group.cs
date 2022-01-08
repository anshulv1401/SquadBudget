using System;

namespace BudgetManager.Models
{
    public class Group
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public double GroupInstallmentAmount { get; set; }
        public int InstallmentDayOfMonth { get; set; }
        public double DefaultLoanInterest { get; set; }
        public int DefaultNoOfInstallment { get; set; }
        public double GroupInstallmentDelayFine { get; set; }
        public int GroupInstallmentDelayFineType { get; set; }
        public int GroupInstallmentDelayFineTerm { get; set; }
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
