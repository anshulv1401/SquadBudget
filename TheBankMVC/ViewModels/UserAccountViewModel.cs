using BudgetManager.Models;
using System.Collections.Generic;
using System.ComponentModel;

namespace BudgetManager.ViewModels
{
    public class UserAccountViewModel
    {
        public int GroupId { get; set; }

        [DisplayName("Squad")]
        public IEnumerable<Group> Groups { get; set; }
        public int UserAccountId { get; set; }

        [DisplayName("Member")]
        public string UserAccountName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }

        [DisplayName("Share")]
        public double ShareSubmitted { get; set; }

        [DisplayName("Fine")]
        public double FineSubmitted { get; set; }

        [DisplayName("Interest")]
        public double InterestSubmitted { get; set; }
        public bool IsActive { get; set; }

        [DisplayName("Loan")]
        public double AmountOnLoan { get; set; }
    }
}
