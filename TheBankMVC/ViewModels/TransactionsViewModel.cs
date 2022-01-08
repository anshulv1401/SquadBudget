using BudgetManager.Models;
using System;
using System.Collections.Generic;

namespace BudgetManager.ViewModels
{
    public class TransactionsViewModel
    {
        public int TransactionId { get; set; }
        public int GroupId { get; set; }
        public int UserAccountId { get; set; }
        public int TransactionTypeId { get; set; }
        public double TransactionAmount { get; set; }
        public DateTime TransactionDate { get; set; }
        public string ReferenceType { get; set; }
        public int ReferenceTypeId { get; set; }
        public string TransactionRemark { get; set; }
        public IEnumerable<Group> Groups { get; set; }
        public IEnumerable<UserAccount> UserAccounts { get; set; }
    }
}
