using System;

namespace BudgetManager.Models
{
    public class Enumeration
    {
        public int EnumerationId { get; set; }
        public string EnumerationType { get; set; }
        public string EnumerationName { get; set; }
        public int EnumerationValue { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }

        public enum TransactionType
        {
            Credit = 1,
            Debit = 2
        }

        public enum CreditRefType
        {
            IndividualLoan = 1,
            GroupWithdrawal = 2,
            Difference = 3
        }

        public enum DebitRefType
        {
            LoanPrinciple = 1,
            LoanInterest = 2,
            GroupInstallment = 3,
            LoanEMIFine = 4,
            GroupInstallmentFine = 5,
            Difference = 6
        }

        public enum UserType
        {
            Admin = 1,
            Member = 2
        }

        public enum DelayFineType
        {
            Amount = 1,
            Percentage = 2
        }

        public enum DelayFineTerm
        {
            PerDay = 1,
            PerMonth = 2
        }

        public enum InterestTerm
        {
            FixedEMI = 1,
            FixedPrincipal = 2,
            BeforeLoan = 3
        }

        public enum InstallmentStatus
        {
            Due = 1,
            Paid = 2,
            Late = 3
        }

        public enum LoanStatus
        {
            Pending = 1,
            Completed = 2
        }

        public enum EMIType
        {
            GroupInstallment = 1,
            LoanEMI = 2
        }
    }
}
