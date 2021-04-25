using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBankMVC.Models;

namespace TheBankMVC.ViewModels
{
    public class TransactionsViewModel
    {
        public int TransactionId { get; set; }
        public int BankId { get; set; }
        public int AccountId { get; set; }
        public int TransactionTypeId { get; set; }
        public double TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ReferenceType { get; set; }
        public int ReferenceTypeId { get; set; }
        public string TransactionRemark { get; set; }
        public IEnumerable<Bank> Banks { get; set; }
        public IEnumerable<UserAccount> UserAccounts { get; set; }
    }
}
