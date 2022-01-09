using System;
using System.ComponentModel;

namespace BudgetManager.ViewModels
{
    public class EMIHeaderViewModel
    {
        public int EMIHeaderId { get; set; }
        public int GroupId { get; set; }
        public int UserAccountId { get; set; }
        public int EMIType { get; set; }
        public double EMIAmount { get; set; }
        public int InstallmentDayOfMonth { get; set; }
        public double LoanAmount { get; set; }
        public double MonthlyRateOfInterest { get; set; }
        public int NoOfInstallment { get; set; }
        public int LockInPeriod { get; set; }
        public int LoanStatus { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int InterestTermId { get; set; }

        [DisplayName("SquadName")]
        public string GroupName { get; set; }

        [DisplayName("MemberName")]
        public string UserAccountName { get; set; }
    }
}
