using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBankMVC.Models;

namespace TheBankMVC.ViewModels
{
    public class UserAccountViewModel
    {
        public int BankId { get; set; }
        public IEnumerable<Bank> Banks { get; set; }
        public int UserAccountId { get; set; }
        public string UserAccountName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public double ShareSubmitted { get; set; }
        public double FineSubmitted { get; set; }
        public double InterestSubmitted { get; set; }
        public bool IsActive { get; set; }
        public double AmountOnLoan { get; set; }
    }
}
