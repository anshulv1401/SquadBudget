using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBankMVC.Models
{
    public class UserAccount
    {
        public int BankId { get; set; }
        public int UserAccountId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNo { get; set; }
        public double ShareSubmitted { get; set; }
        public double FineSubmitted { get; set; }
        public double InterestSubmitted { get; set; }
        public bool IsActive { get; set; }
        public double AmountOnLoan { get; set; }
    }
}
