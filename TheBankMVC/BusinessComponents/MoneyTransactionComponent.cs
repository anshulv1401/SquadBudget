using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBankMVC.Data;
using TheBankMVC.Models;

namespace TheBankMVC.BusinessComponents
{
    public class MoneyTransactionComponent
    {
        private readonly ApplicationDbContext _context;

        public MoneyTransactionComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task CreateMoneyTransaction(Transaction transaction)
        {
            switch ((Enumeration.TransactionType)transaction.TransactionTypeId)
            {
                case Enumeration.TransactionType.Credit:
                    CreditTransaction(transaction, _context);
                    break;
                case Enumeration.TransactionType.Debit:
                    DebitTransaction(transaction, _context);
                    break;
                default:
                    throw new NotImplementedException("TransactionType case missing");
            }

            _context.Add(transaction);
            await _context.SaveChangesAsync();
        }

        private async void CreditTransaction(Transaction transaction, ApplicationDbContext _context)
        {
            var userAccount = await _context.UserAccount.FindAsync(transaction.UserAccountId);

            switch((Enumeration.CreditRefType)transaction.ReferenceTypeId)
            {
                case Enumeration.CreditRefType.IndividualLoan:
                    if (userAccount.AmountOnLoan != 0)
                    {
                        throw new NotImplementedException("Loan pending. Validation msg to be implemented");
                    }
                    else
                    {
                        userAccount.AmountOnLoan = transaction.TransactionAmount;
                    }
                    break;
                case Enumeration.CreditRefType.Bankwithdrawal:
                    throw new NotImplementedException("Bankwithdrawal pending");
                default:
                    throw new NotImplementedException("Credit case missing");
            }

            transaction.ReferenceType = ((Enumeration.CreditRefType)transaction.ReferenceTypeId).ToString();
        }

        private async void DebitTransaction(Transaction transaction, ApplicationDbContext _context)
        {
            var userAccount = await _context.UserAccount.FindAsync(transaction.UserAccountId);

            switch ((Enumeration.DebitRefType)transaction.ReferenceTypeId)
            {
                case Enumeration.DebitRefType.LoanPrinciple:
                    userAccount.AmountOnLoan -= transaction.TransactionAmount;
                    break;
                case Enumeration.DebitRefType.LoanInterest:
                    userAccount.InterestSubmitted += transaction.TransactionAmount;
                    break;
                case Enumeration.DebitRefType.BankInstallment:
                    userAccount.ShareSubmitted += transaction.TransactionAmount;
                    break;
                case Enumeration.DebitRefType.LoanEMIFine:
                case Enumeration.DebitRefType.BankInstallmentFine:
                    userAccount.FineSubmitted += transaction.TransactionAmount;
                    break;
                default:
                    throw new NotImplementedException("Debit case missing");
            }

            transaction.ReferenceType = ((Enumeration.DebitRefType)transaction.ReferenceTypeId).ToString();
        }
    }
}
