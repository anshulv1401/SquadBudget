using System.ComponentModel.DataAnnotations;

namespace TheBankMVC.ViewModels
{
    public class EMIConfigViewModel
    {
        [Required]
        public int BankId { get; set; }
        [Required]
        public int UserAccountId { get; set; }
        [Required]
        public double LoanAmount { get; set; }
        [Required]
        public double MonthlyRateOfInterest { get; set; }
        [Required]
        public int NoOfInstallment { get; set; }
        [Required]
        public int LockInPeriod { get; set; }
    }
}
