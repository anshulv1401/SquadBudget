using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBankMVC.Data;
using TheBankMVC.Models;
using TheBankMVC.ViewModels;
using static TheBankMVC.Models.Enumeration;

namespace TheBankMVC.BusinessComponents
{
    public class InstallmentComponent
    {
        private ApplicationDbContext _context { get; set; }
        private MoneyTransactionComponent MoneyTransactionComponent { get; set; }

        public InstallmentComponent(ApplicationDbContext context)
        {
            _context = context;
            MoneyTransactionComponent = new MoneyTransactionComponent(_context);

        }

        public void RefreshInstallmentStatus()
        {
            var pendingEMIs = _context.EMIHeaders.Where(x => x.LoanStatus != (int)LoanStatus.Completed).ToList();

            foreach(var pendingEMI in pendingEMIs)
            {
                var dueDate = GetDueDate(pendingEMI.InstallmentDayOfMonth);

                var pendingInstallments = _context.Installments.Where(x =>
                x.DueDate.Date <= dueDate && 
                x.EMIHeaderId == pendingEMI.EMIHeaderId &&
                x.InstallmentStatus != (int)InstallmentStatus.Paid).ToList();

                foreach (var pendingInstallment in pendingInstallments)
                {
                    if (pendingInstallment.DueDate.Date <= DateTime.Now.Date)
                    {
                        pendingInstallment.InstallmentStatus = (int)InstallmentStatus.Late;
                        pendingInstallment.Fine = GetFine(pendingEMI.DelayFine, pendingEMI.DelayFineType, pendingEMI.DelayFinePeriod, dueDate);
                    }
                }
            }

            //TODO Create Bank Installments using date Cycle 
            _context.SaveChanges();
        }

        public List<Installment> GetInstallments(EMIDetailsViewModel eMIDetails)
        {
            var bankId = eMIDetails.EMIHeader.BankId;
            var userAccountId = eMIDetails.EMIHeader.UserAccountId;
            var eMIType = eMIDetails.EMIHeader.EMIType;
            var installments = new List<Installment>();
            var dateOfInstallment = eMIDetails.EMIHeader.StartTime.Date;
            var opening = eMIDetails.EMIHeader.LoanAmount;
            var eMIAmount = eMIDetails.EMIHeader.EMIAmount;
            var interestAmount = opening * (eMIDetails.EMIHeader.MonthlyRateOfInterest / 100);
            var principalAmount = eMIAmount - interestAmount;
            var closing = opening - principalAmount;
            var difference = 0.0;


            for (int i = 1; i <= eMIDetails.EMIHeader.NoOfInstallment; i++)
            {
                var installment = new Installment()
                {
                    InstallmentNo = i,
                    DueDate = dateOfInstallment,
                    Opening = opening,
                    PrincipalAmount = principalAmount,
                    InterestAmount = interestAmount,
                    EMIAmount = eMIAmount,
                    Closing = closing - difference,
                    Difference = difference,
                    //*new variables added*//
                    BankId = bankId,
                    UserAccountId = userAccountId,
                    EMIType = eMIType,
                    InstallmentStatus = (int)InstallmentStatus.Due,
                    Fine = 0
                };

                installments.Add(installment);

                dateOfInstallment = dateOfInstallment.AddMonths(1);
                opening = closing;
                interestAmount = opening * (eMIDetails.EMIHeader.MonthlyRateOfInterest / 100);
                principalAmount = eMIAmount - interestAmount;
                closing = opening - principalAmount;

                if (i == eMIDetails.EMIHeader.NoOfInstallment - 1)//for last EMI
                {
                    difference = closing;
                }
            }
            return installments;
        }


        public async Task PayInstallmentTransaction(Installment installment)
        {
            var transactions = new List<Transaction>();

            if (installment.EMIType == (int)EMIType.BankInstallment)
            {
                var transaction1 = new Transaction
                {
                    BankId = installment.BankId,
                    UserAccountId = installment.UserAccountId,
                    TransactionTypeId = (int)TransactionType.Debit,
                    TransactionAmount = installment.EMIAmount,
                    TransactionDate = DateTime.Now,
                    ReferenceType = DebitRefType.BankInstallment.ToString(),
                    ReferenceTypeId = (int)DebitRefType.BankInstallment
                };

                transactions.Add(transaction1);

                if (installment.Fine != 0)
                {
                    var transaction2 = new Transaction
                    {
                        BankId = installment.BankId,
                        UserAccountId = installment.UserAccountId,
                        TransactionTypeId = (int)TransactionType.Debit,
                        TransactionAmount = installment.Fine,
                        TransactionDate = DateTime.Now,
                        ReferenceType = DebitRefType.BankInstallmentFine.ToString(),
                        ReferenceTypeId = (int)DebitRefType.BankInstallmentFine
                    };

                    transactions.Add(transaction2);
                }
            }
            else if (installment.EMIType == (int)EMIType.LoanEMI)
            {
                var transaction1 = new Transaction
                {
                    BankId = installment.BankId,
                    UserAccountId = installment.UserAccountId,
                    TransactionTypeId = (int)TransactionType.Debit,
                    TransactionAmount = installment.PrincipalAmount,
                    TransactionDate = DateTime.Now,
                    ReferenceType = DebitRefType.LoanPrinciple.ToString(),
                    ReferenceTypeId = (int)DebitRefType.LoanPrinciple
                };

                transactions.Add(transaction1);

                var transaction2 = new Transaction
                {
                    BankId = installment.BankId,
                    UserAccountId = installment.UserAccountId,
                    TransactionTypeId = (int)TransactionType.Debit,
                    TransactionAmount = installment.InterestAmount,
                    TransactionDate = DateTime.Now,
                    ReferenceType = DebitRefType.LoanInterest.ToString(),
                    ReferenceTypeId = (int)DebitRefType.LoanInterest
                };

                transactions.Add(transaction2);

                if (installment.Difference != 0)
                {
                    var transaction3 = new Transaction
                    {
                        BankId = installment.BankId,
                        UserAccountId = installment.UserAccountId,
                        TransactionTypeId = (int)TransactionType.Debit,
                        TransactionAmount = installment.Difference,
                        TransactionDate = DateTime.Now,
                        ReferenceType = DebitRefType.Difference.ToString(),
                        ReferenceTypeId = (int)DebitRefType.Difference
                    };

                    transactions.Add(transaction3);
                }

                if (installment.Fine != 0)
                {
                    var transaction4 = new Transaction
                    {
                        BankId = installment.BankId,
                        UserAccountId = installment.UserAccountId,
                        TransactionTypeId = (int)TransactionType.Debit,
                        TransactionAmount = installment.Fine,
                        TransactionDate = DateTime.Now,
                        ReferenceType = DebitRefType.LoanEMIFine.ToString(),
                        ReferenceTypeId = (int)DebitRefType.LoanEMIFine
                    };

                    transactions.Add(transaction4);
                }
            }
            else
            {
                throw new NotImplementedException("PayInstallment Case missing");
            }

            await MoneyTransactionComponent.CreateMoneyTransactions(transactions);
            installment.InstallmentStatus = (int)InstallmentStatus.Paid;

            UpdateEMIIfCompleted(installment);
            await UpdateUserAccountAmountOnLonaIfDiscrepancyAsync(installment.UserAccountId);

            _context.SaveChanges();
        }

        public async Task SaveInstallmentsAsync(EMIHeader eMIHeader, List<Installment> installments)
        {
            var userAccount = _context.UserAccount.Where(x => x.UserAccountId == eMIHeader.UserAccountId).First();

            if (userAccount.AmountOnLoan != 0)
            {
                throw new NotImplementedException("Active loan pending, Validation to be implemented");
            }

            using var dbTransaction = _context.Database.BeginTransaction();
            try
            {
                _context.EMIHeaders.Add(eMIHeader);
                _context.SaveChanges();

                installments.ForEach(x => x.EMIHeaderId = eMIHeader.EMIHeaderId);

                _context.Installments.AddRange(installments);
                _context.SaveChanges();

                dbTransaction.Commit();
            }
            catch (Exception ex)
            {
                dbTransaction.Rollback();
                //TODO log the error
                throw ex;
            }

            var moneytransaction = new Transaction
            {
                BankId = eMIHeader.BankId,
                UserAccountId = eMIHeader.UserAccountId,
                TransactionTypeId = (int)TransactionType.Credit,
                TransactionAmount = eMIHeader.LoanAmount,
                TransactionDate = DateTime.Now,
                ReferenceType = CreditRefType.IndividualLoan.ToString(),
                ReferenceTypeId = (int)CreditRefType.IndividualLoan
            };

            await MoneyTransactionComponent.CreateMoneyTransaction(moneytransaction);
        }

        public DateTime GetDueDate(int dayOfMonth)
        {
            DateTime dueDate;
            
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var dateOfMonth = firstDayOfMonth.AddDays(dayOfMonth - 1).Date;

            if (DateTime.Now.Month < dateOfMonth.Month)//dayOfMonth has went to next month. case (dayOfMonth is 31 and the month is of 30 days)
            {
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                dueDate = lastDayOfMonth.Date;
            }
            else
            {
                if (DateTime.Now.Date <= dateOfMonth.Date)// date of the month has not passed
                {
                    dueDate = dateOfMonth.Date;
                }
                else//present date has passed the dayOfMonth. hance taking next months date
                {
                    dueDate = dateOfMonth.AddMonths(1).Date;
                }
            }
            return dueDate.Date.AddMonths(2);
        }



        #region privateMethods
        
        private double GetFine(double delayFine, int delayFineType, int delayFineTerm, DateTime dueDate)
        {
            //TODO implement fines logic
            return 1;
        }

        private void UpdateEMIIfCompleted(Installment installment)
        {
            var pendingInstallmentCount = _context.Installments.Where(x => x.EMIHeaderId == installment.EMIHeaderId
            && x.InstallmentStatus != (int)InstallmentStatus.Paid).ToList().Count();

            if (pendingInstallmentCount == 0)
            {
                var eMIHeader = _context.EMIHeaders.Where(x => x.EMIHeaderId == installment.EMIHeaderId).First();
                eMIHeader.LoanStatus = (int)LoanStatus.Completed;
            }
        }

        private async Task UpdateUserAccountAmountOnLonaIfDiscrepancyAsync(int userAccountId)
        {
            var userAccount = _context.UserAccount.Where(x => x.UserAccountId == userAccountId).First();
            
            var transactions = new List<Transaction>();

            if (userAccount.AmountOnLoan < 0 && userAccount.AmountOnLoan > -1)
            {
                var transaction = new Transaction
                {
                    BankId = userAccount.BankId,
                    UserAccountId = userAccount.UserAccountId,
                    TransactionTypeId = (int)TransactionType.Credit,
                    TransactionAmount = userAccount.AmountOnLoan,
                    TransactionDate = DateTime.Now,
                    ReferenceType = CreditRefType.Difference.ToString(),
                    ReferenceTypeId = (int)CreditRefType.Difference
                };

                await MoneyTransactionComponent.CreateMoneyTransaction(transaction);
            }
            else if(userAccount.AmountOnLoan > 0 && userAccount.AmountOnLoan < 1)
            {
                var transaction = new Transaction
                {
                    BankId = userAccount.BankId,
                    UserAccountId = userAccount.UserAccountId,
                    TransactionTypeId = (int)TransactionType.Debit,
                    TransactionAmount = userAccount.AmountOnLoan,
                    TransactionDate = DateTime.Now,
                    ReferenceType = DebitRefType.Difference.ToString(),
                    ReferenceTypeId = (int)DebitRefType.Difference
                };

                await MoneyTransactionComponent.CreateMoneyTransaction(transaction);
            }
            else if(userAccount.AmountOnLoan < -1)
            {
                throw new NotImplementedException("AmountOnLoan less then -1, Error msg to be implemented");
            }
        }

        #endregion
    }
}
