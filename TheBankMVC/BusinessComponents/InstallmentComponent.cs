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

        public bool RefreshInstallments()
        {
            bool installmentChanged = false;

            //Get new installments
            var banks = _context.Bank.ToList();
            foreach (var bank in banks)
            {
                var dueDate = GetDueDate(bank.InstallmentDayOfMonth);
                var userAccounts = _context.UserAccount.Where(x => x.BankId == bank.BankId).ToList();
                foreach (var userAccount in userAccounts)
                {
                    var eMIHeaderCount = _context.EMIHeaders.Where(x =>
                    x.BankId == userAccount.BankId &&
                    x.UserAccountId == userAccount.UserAccountId &&
                    x.StartTime.Date >= DateTime.Now.Date && //next installment
                    x.EMIType == ((int)EMIType.BankInstallment)
                    ).Count();

                    if (eMIHeaderCount == 0)
                    {
                        GenerateBankInstallments(bank.BankId, userAccount.UserAccountId, bank.BankInstallmentAmount);
                        installmentChanged = true;
                    }
                }
            }
            _context.SaveChanges();

            //Update fines for old installments
            var pendingEMIs = _context.EMIHeaders.Where(x => x.LoanStatus != (int)LoanStatus.Completed).ToList();

            foreach (var pendingEMI in pendingEMIs)
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
                        var oldFine = pendingInstallment.Fine;
                        pendingInstallment.InstallmentStatus = (int)InstallmentStatus.Late;
                        pendingInstallment.Fine = GetFine(pendingEMI.DelayFine, pendingEMI.DelayFineType, pendingEMI.DelayFinePeriod, pendingInstallment.DueDate, (pendingInstallment.EMIAmount + pendingInstallment.Difference));

                        if (oldFine != pendingInstallment.Fine)
                        {
                            installmentChanged = true;
                        }
                    }
                }
            }

            _context.SaveChanges();

            return installmentChanged;
        }

        public void GenerateBankInstallments(int bankId, int userAccountId, double installmentAmt)
        {   
            EMIConfigViewModel eMIConfig = new EMIConfigViewModel()
            {
                BankId = bankId,
                UserAccountId = userAccountId,
                LoanAmount = installmentAmt,
                MonthlyRateOfInterest = 0.001,
                NoOfInstallment = 1,
                EMIType = (int)EMIType.BankInstallment
            };

            var eMIHeader = GetEMIHeader(eMIConfig);
            var installments = GetInstallments(eMIHeader);

            SaveInstallments(eMIHeader, installments);
        }

        public EMIHeader GetEMIHeader(EMIConfigViewModel eMIConfig)
        {
            //EMI calculation
            var r = eMIConfig.MonthlyRateOfInterest / 100;//Monthly RateOfInterest
            var t = eMIConfig.NoOfInstallment;
            var p = eMIConfig.LoanAmount;

            var rPlus1PowN = Math.Pow((1 + r), t);
            var emi = (int)((p * r * rPlus1PowN) / (rPlus1PowN - 1));
            //EMI calculation

            var bank = _context.Bank.Where(x => x.BankId == eMIConfig.BankId).First();
            var startDate = GetDueDate(bank.InstallmentDayOfMonth);

            double DelayFine;
            int DelayFineType;
            int DelayFinePeriod;

            if (eMIConfig.EMIType == (int)EMIType.BankInstallment)
            {
                DelayFine = bank.BankInstallmentDelayFine;
                DelayFineType = bank.BankInstallmentDelayFineType;
                DelayFinePeriod = bank.BankInstallmentDelayFineTerm;
            }
            else
            {
                DelayFine = bank.LoanDelayFine;
                DelayFineType = bank.LoanDelayFineType;
                DelayFinePeriod = bank.LoanDelayFineTerm;
            }

            var eMIHeader = new EMIHeader()
            {
                EMIAmount = emi,
                LoanAmount = eMIConfig.LoanAmount,
                MonthlyRateOfInterest = eMIConfig.MonthlyRateOfInterest,
                NoOfInstallment = eMIConfig.NoOfInstallment,
                LockInPeriod = eMIConfig.LockInPeriod,
                StartTime = startDate,
                EndTime = startDate.AddMonths(eMIConfig.NoOfInstallment),
                //*new Variables Added*//
                BankId = eMIConfig.BankId,
                UserAccountId = eMIConfig.UserAccountId,
                EMIType = eMIConfig.EMIType,
                LoanStatus = (int)Enumeration.LoanStatus.Pending,
                InstallmentDayOfMonth = bank.InstallmentDayOfMonth,
                DelayFine = DelayFine,
                DelayFineType = DelayFineType,
                DelayFinePeriod = DelayFinePeriod,
                InterestTermId = bank.InterestTermID
            };

            return eMIHeader;
        }

        public List<Installment> GetInstallments(EMIHeader eMIHeader)
        {
            var bankId = eMIHeader.BankId;
            var userAccountId = eMIHeader.UserAccountId;
            var eMIType = eMIHeader.EMIType;
            var installments = new List<Installment>();
            var dateOfInstallment = eMIHeader.StartTime.Date.AddMonths(1);
            var opening = eMIHeader.LoanAmount;
            var eMIAmount = eMIHeader.EMIAmount;
            var interestAmount = opening * (eMIHeader.MonthlyRateOfInterest / 100);
            var principalAmount = eMIAmount - interestAmount;
            var closing = opening - principalAmount;
            var difference = 0.0;


            for (int i = 1; i <= eMIHeader.NoOfInstallment; i++)
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
                interestAmount = opening * (eMIHeader.MonthlyRateOfInterest / 100);
                principalAmount = eMIAmount - interestAmount;
                closing = opening - principalAmount;

                if (i == eMIHeader.NoOfInstallment - 1)//for last EMI
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
            _context.SaveChanges();

            UpdateEMIIfCompleted(installment);
            await UpdateUserAccountAmountOnLonaIfDiscrepancyAsync(installment.UserAccountId);

            _context.SaveChanges();
        }

        public void SaveInstallments(EMIHeader eMIHeader, List<Installment> installments)
        {
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
            return dueDate.Date;
        }



        #region privateMethods

        private double GetFine(double delayFine, int delayFineType, int delayFineTerm, DateTime dueDate, double? fineOnAmt = null)
        {
            int fineMultiplierFloor = 0;

            if (delayFineTerm == (int)DelayFineTerm.PerDay)
            {
                fineMultiplierFloor = DateTime.Now.Date.Subtract(dueDate.Date).Days;
            }
            else if (delayFineTerm == (int)DelayFineTerm.PerMonth)
            {
                //fineMultiplierFloor = (int)(DateTime.Now.Date.Subtract(dueDate.Date).Days / (365.2425 / 12));
                //var v1 = ((DateTime.Now.Date.Year - dueDate.Year) * 12) + DateTime.Now.Date.Month - dueDate.Month;

                for(DateTime dateTemp = dueDate.Date; DateTime.Now.Date > dateTemp.Date; dateTemp = dateTemp.AddMonths(1))
                {
                    fineMultiplierFloor++;
                }
            }
            else
            {
                throw new NotImplementedException("FineTerm Case missing");
            }

            if (delayFineType == (int)DelayFineType.Amount)
            {
                return (delayFine * fineMultiplierFloor);
            }
            else if (delayFineType == (int)DelayFineType.Percentage)
            {
                if (fineOnAmt == null || fineOnAmt == 0)
                {
                    throw new NotImplementedException("for percentage Fine, fineOnAmt cannot be 0");
                }
                var fineAmt = ((delayFine * fineOnAmt) / 100);

                return (fineAmt.Value * fineMultiplierFloor);
            }
            else
            {
                throw new NotImplementedException("FineType Case missing");
            }
        }

        private void UpdateEMIIfCompleted(Installment installment)
        {
            var pendingInstallmentCount = _context.Installments.Where(x => x.EMIHeaderId == installment.EMIHeaderId
            && x.InstallmentStatus != (int)InstallmentStatus.Paid).ToList().Count();

            if (pendingInstallmentCount == 0)
            {
                var eMIHeader = _context.EMIHeaders.Where(x => x.EMIHeaderId == installment.EMIHeaderId).First();
                eMIHeader.LoanStatus = (int)LoanStatus.Completed;
                _context.SaveChanges();
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
                _context.SaveChanges();
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
                _context.SaveChanges();
            }
            else if(userAccount.AmountOnLoan < -1)
            {
                throw new NotImplementedException("AmountOnLoan less then -1, Error msg to be implemented");
            }
        }

        #endregion
    }
}
