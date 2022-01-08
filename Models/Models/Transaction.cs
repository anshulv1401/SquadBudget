using System;

namespace BudgetManager.Models
{
    public class Transaction
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
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
