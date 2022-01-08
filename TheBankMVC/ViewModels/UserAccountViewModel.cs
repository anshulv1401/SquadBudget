using BudgetManager.Models;
using System.Collections.Generic;

namespace BudgetManager.ViewModels
{
    public class UserAccountViewModel
    {
        public int GroupId { get; set; }
        public IEnumerable<Group> Groups { get; set; }
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
