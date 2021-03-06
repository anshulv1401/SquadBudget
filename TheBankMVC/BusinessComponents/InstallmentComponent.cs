using BudgetManager.Data;
using BudgetManager.Models;
using BudgetManager.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BudgetManager.Enumerations.Enumeration;

namespace BudgetManager.BusinessComponents
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
            var groups = _context.Groups.ToList();
            foreach (var group in groups)
            {
                var dueDate = GetDueDate(group.InstallmentDayOfMonth);
                var userAccounts = _context.UserAccounts.Where(x => x.GroupId == group.GroupId).ToList();

                foreach (var userAccount in userAccounts)
                {
                    var lastEMIHeader = _context.EMIHeaders.Where(x =>
                    x.GroupId == userAccount.GroupId &&
                    x.UserAccountId == userAccount.UserAccountId &&
                    x.EMIType == ((int)EMIType.Budget)
                    ).OrderBy(x => x.StartTime).LastOrDefault();


                    if (lastEMIHeader != null)//Generating EMIs for the missed months
                    {
                        var lastEMIStartDate = lastEMIHeader.StartTime;

                        var dateDiff = dueDate - lastEMIStartDate;

                        while (dateDiff.TotalDays > 31)
                        {
                            var newDueDate = new DateTime(dueDate.Year, lastEMIStartDate.Month + 1, dueDate.Day);

                            GenerateGroupInstallments(group.GroupId, userAccount.UserAccountId, group.GroupInstallmentAmount, newDueDate);
                            installmentChanged = true;
                            lastEMIStartDate = newDueDate;
                            dateDiff = dueDate - lastEMIStartDate;
                        }
                    }
                    else//Generating EMIs for current month. This happens only for the first time.
                    {
                        var previousMonthDueDate = dueDate.AddMonths(-1);
                        GenerateGroupInstallments(group.GroupId, userAccount.UserAccountId, group.GroupInstallmentAmount, previousMonthDueDate);
                        installmentChanged = true;
                    }

                    //Generating for the Next month
                    var eMIHeadersForCurrentMonth = _context.EMIHeaders.Where(x =>
                    x.GroupId == userAccount.GroupId &&
                    x.UserAccountId == userAccount.UserAccountId &&
                    x.StartTime.Date >= DateTime.Now.Date && //next installment
                    x.EMIType == ((int)EMIType.Budget)
                    );

                    if (eMIHeadersForCurrentMonth.Count() == 0)
                    {
                        GenerateGroupInstallments(group.GroupId, userAccount.UserAccountId, group.GroupInstallmentAmount, dueDate);
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

        public void GenerateGroupInstallments(int groupId, int userAccountId, double installmentAmt, DateTime startDate)
        {
            EMIConfigViewModel eMIConfig = new EMIConfigViewModel()
            {
                GroupId = groupId,
                UserAccountId = userAccountId,
                LoanAmount = installmentAmt,
                MonthlyRateOfInterest = 0.001,
                NoOfInstallment = 1,
                EMIType = (int)EMIType.Budget
            };

            var eMIHeader = GetEMIHeader(eMIConfig, startDate);
            var installments = GetInstallments(eMIHeader);

            SaveInstallments(eMIHeader, installments);
        }

        public EMIHeader GetEMIHeader(EMIConfigViewModel eMIConfig, DateTime startDate)
        {
            //EMI calculation
            var r = eMIConfig.MonthlyRateOfInterest / 100;//Monthly RateOfInterest
            var t = eMIConfig.NoOfInstallment;
            var p = eMIConfig.LoanAmount;

            var rPlus1PowN = Math.Pow((1 + r), t);
            var emi = (int)((p * r * rPlus1PowN) / (rPlus1PowN - 1));
            //EMI calculation

            var group = _context.Groups.Where(x => x.GroupId == eMIConfig.GroupId).First();

            double DelayFine;
            int DelayFineType;
            int DelayFinePeriod;

            if (eMIConfig.EMIType == (int)EMIType.Budget)
            {
                DelayFine = group.GroupInstallmentDelayFine;
                DelayFineType = group.GroupInstallmentDelayFineType;
                DelayFinePeriod = group.GroupInstallmentDelayFineTerm;
            }
            else
            {
                DelayFine = group.LoanDelayFine;
                DelayFineType = group.LoanDelayFineType;
                DelayFinePeriod = group.LoanDelayFineTerm;
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
                GroupId = eMIConfig.GroupId,
                UserAccountId = eMIConfig.UserAccountId,
                EMIType = eMIConfig.EMIType,
                LoanStatus = (int)LoanStatus.Pending,
                InstallmentDayOfMonth = group.InstallmentDayOfMonth,
                DelayFine = DelayFine,
                DelayFineType = DelayFineType,
                DelayFinePeriod = DelayFinePeriod,
                InterestTermId = group.InterestTermID
            };

            return eMIHeader;
        }

        public List<Installment> GetInstallments(EMIHeader eMIHeader)
        {
            var groupId = eMIHeader.GroupId;
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
                    GroupId = groupId,
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

            if (installment.EMIType == (int)EMIType.Budget)
            {
                var transaction1 = new Transaction
                {
                    GroupId = installment.GroupId,
                    UserAccountId = installment.UserAccountId,
                    TransactionTypeId = (int)TransactionType.Debit,
                    TransactionAmount = installment.EMIAmount,
                    TransactionDate = DateTime.Now,
                    ReferenceType = DebitRefType.BudgetInstallment.ToString(),
                    ReferenceTypeId = (int)DebitRefType.BudgetInstallment
                };

                transactions.Add(transaction1);

                if (installment.Fine != 0)
                {
                    var transaction2 = new Transaction
                    {
                        GroupId = installment.GroupId,
                        UserAccountId = installment.UserAccountId,
                        TransactionTypeId = (int)TransactionType.Debit,
                        TransactionAmount = installment.Fine,
                        TransactionDate = DateTime.Now,
                        ReferenceType = DebitRefType.BudgetInstallmentFine.ToString(),
                        ReferenceTypeId = (int)DebitRefType.BudgetInstallmentFine
                    };

                    transactions.Add(transaction2);
                }
            }
            else if (installment.EMIType == (int)EMIType.Loan)
            {
                var transaction1 = new Transaction
                {
                    GroupId = installment.GroupId,
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
                    GroupId = installment.GroupId,
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
                        GroupId = installment.GroupId,
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
                        GroupId = installment.GroupId,
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
                throw;
            }
        }

        public DateTime GetDueDate(int cycleDayOfMonth)//21
        {
            DateTime dueDate;

            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var cycleDateOfMonth = firstDayOfMonth.AddDays(cycleDayOfMonth - 1).Date;

            if (DateTime.Now.Month < cycleDateOfMonth.Month)//cycleDateOfMonth has went to next month. case (cycleDateOfMonth is 31 and the month is of 30 days)
            {
                //return the last date of the month as Due date
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                dueDate = lastDayOfMonth.Date;
            }
            else
            {
                if (DateTime.Now.Date <= cycleDateOfMonth.Date)// date of the month has not passed
                {
                    dueDate = cycleDateOfMonth.Date;
                }
                else//present date has passed the dayOfMonth. hance taking next months date
                {
                    dueDate = cycleDateOfMonth.AddMonths(1).Date;
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
                for (DateTime dateTemp = dueDate.Date; DateTime.Now.Date > dateTemp.Date; dateTemp = dateTemp.AddMonths(1))
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
            var userAccount = _context.UserAccounts.Where(x => x.UserAccountId == userAccountId).First();

            var transactions = new List<Transaction>();

            if (userAccount.AmountOnLoan < 0 && userAccount.AmountOnLoan > -1)
            {
                var transaction = new Transaction
                {
                    GroupId = userAccount.GroupId,
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
            else if (userAccount.AmountOnLoan > 0 && userAccount.AmountOnLoan < 1)
            {
                var transaction = new Transaction
                {
                    GroupId = userAccount.GroupId,
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
            else if (userAccount.AmountOnLoan < -1)
            {
                throw new NotImplementedException("AmountOnLoan less then -1, Error msg to be implemented");
            }
        }

        #endregion
    }
}
