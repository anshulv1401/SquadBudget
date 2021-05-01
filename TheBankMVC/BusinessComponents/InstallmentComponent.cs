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

        public InstallmentComponent(ApplicationDbContext context)
        {
            _context = context;
        }

        public void RefreshInstallmentStatus()
        {
            var bank = _context.Bank.First();
            var dueDate = GetDueDate(bank.InstallmentDayOfMonth);

            var installments = _context.Installments.Where(x => x.DueDate.Date <= dueDate && x.InstallmentStatus != (int)Enumeration.InstallmentStatus.Paid).ToList();

            foreach(var installment in installments)
            {
                var eMIHeader = _context.EMIHeaders.Where(x => x.EMIHeaderId == installment.EMIHeaderId).First();

                if (installment.DueDate.Date <= DateTime.Now.Date)
                {
                    installment.InstallmentStatus = (int)Enumeration.InstallmentStatus.Late;
                    installment.Fine = GetFine(eMIHeader.DelayFine, eMIHeader.DelayFineType, eMIHeader.DelayFinePeriod, dueDate);
                }
                else
                {
                    installment.InstallmentStatus = (int)Enumeration.InstallmentStatus.Due;
                    installment.Fine = 0;
                }
            }
            _context.SaveChanges();
        }

        public List<Installment> GetInstallments(EMIViewModel eMIDetails)
        {
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
                    Difference = difference
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


        public DateTime GetDueDate(int dayOfMonth)
        {
            DateTime dueDate;
            
            var firstDayOfMonth = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
            var dateOfMonth = firstDayOfMonth.AddDays(dayOfMonth).Date;

            if (DateTime.Now.Month < dateOfMonth.Month)//dayOfMonth has went to next month. case (dayOfMonth is 31 and the month is of 30 days)
            {
                var lastDayOfMonth = firstDayOfMonth.AddMonths(1).AddDays(-1);
                dueDate = lastDayOfMonth.Date;
            }
            else
            {
                if (DateTime.Now.Date <= dateOfMonth.Date)
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

        private double GetFine(double delayFine, int delayFineType, int delayFineTerm, DateTime dueDate)
        {
            //TODO implement fines logic
            return 1;
        }
    }
}
