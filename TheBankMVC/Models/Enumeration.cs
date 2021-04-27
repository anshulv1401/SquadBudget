using System.Collections.Generic;
using System.Linq;
using TheBankMVC.Data;

namespace TheBankMVC.Models
{
    public class Enumeration
    {
        public int EnumerationId { get; set; }
        public string EnumerationType { get; set; }
        public string EnumerationName { get; set; }
        public int EnumerationValue { get; set; }

        public enum TransactionType
        {
            Credit = 1,
            Debit = 2
        }

        public enum TransactionRefType
        {
            Loan = 1,
            Bank = 2
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

        public static List<Enumeration> GetEnumerations(string enumeration, ApplicationDbContext context)
        {
            return context.Enumerations.Where(x => x.EnumerationType.ToLower() == enumeration.ToLower()).ToList();
        }
    }
}
