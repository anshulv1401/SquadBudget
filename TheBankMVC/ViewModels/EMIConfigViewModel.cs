using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheBankMVC.Models;

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
        public int EMIType { get; set; }
        public IEnumerable<Bank> Banks { get; set; }
        public IEnumerable<UserAccount> UserAccounts { get; set; }
    }
}
