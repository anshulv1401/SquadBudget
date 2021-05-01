using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using TheBankMVC.Data;
using TheBankMVC.Models;
using TheBankMVC.ViewModels;
using TheBankMVC.BusinessComponents;
using System.Threading.Tasks;

namespace TheBankMVC.Controllers
{
    public class EMIController : Controller
    {

        private readonly ApplicationDbContext _context;
        private readonly InstallmentComponent installmentComponent;

        public EMIController(ApplicationDbContext context)
        {
            _context = context;
            installmentComponent = new InstallmentComponent(_context);
        }

        public ActionResult Index()
        {
            var eMIHeaderList = _context.EMIHeaders.ToList();

            var eMIHeaderViewModelList = new List<EMIHeaderViewModel>();
            foreach(var eMIHeader in eMIHeaderList)
            {
                var eMIHeaderViewModel = new EMIHeaderViewModel();
                
                eMIHeaderViewModel.EMIHeaderId = eMIHeader.EMIHeaderId;
                eMIHeaderViewModel.BankId = eMIHeader.BankId;
                eMIHeaderViewModel.UserAccountId = eMIHeader.UserAccountId;
                eMIHeaderViewModel.EMIType = eMIHeader.EMIType;
                eMIHeaderViewModel.EMIAmount = eMIHeader.EMIAmount;
                eMIHeaderViewModel.InstallmentDayOfMonth = eMIHeader.InstallmentDayOfMonth;
                eMIHeaderViewModel.LoanAmount = eMIHeader.LoanAmount;
                eMIHeaderViewModel.MonthlyRateOfInterest = eMIHeader.MonthlyRateOfInterest;
                eMIHeaderViewModel.NoOfInstallment = eMIHeader.NoOfInstallment;
                eMIHeaderViewModel.LockInPeriod = eMIHeader.LockInPeriod;
                eMIHeaderViewModel.LoanStatus = eMIHeader.LoanStatus;
                eMIHeaderViewModel.StartTime = eMIHeader.StartTime;
                eMIHeaderViewModel.EndTime = eMIHeader.EndTime;
                eMIHeaderViewModel.InterestTermId = eMIHeader.InterestTermId;
                eMIHeaderViewModel.BankName = _context.Bank.Where(x => x.BankId == eMIHeader.BankId).First().BankName;
                eMIHeaderViewModel.UserAccountName = _context.UserAccount.Where(x => x.UserAccountId == eMIHeader.UserAccountId).First().UserAccountName;

                eMIHeaderViewModelList.Add(eMIHeaderViewModel);
            }

            return View("List", eMIHeaderViewModelList);
        }

        public ActionResult New()
        {
            var eMIConfigViewModel = new EMIConfigViewModel()
            {
                Banks = _context.Bank.ToList()
            };
            return View("EMIConfig", eMIConfigViewModel);
        }

        public async Task<IActionResult> Save(EMIDetailsViewModel eMIDetails)
        {
            await installmentComponent.SaveInstallmentsAsync(eMIDetails.EMIHeader, eMIDetails.Installments);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult EMIDetails(EMIConfigViewModel eMIConfig)
        {
            var bank = _context.Bank.Where(x => x.BankId == eMIConfig.BankId).First();
            var r = eMIConfig.MonthlyRateOfInterest / 100;//Monthly RateOfInterest
            var t = eMIConfig.NoOfInstallment;
            var p = eMIConfig.LoanAmount;

            var rPlus1PowN = Math.Pow((1 + r), t);
            var emi = (int)((p * r * rPlus1PowN) / (rPlus1PowN - 1));

            var dueDate = installmentComponent.GetDueDate(bank.InstallmentDayOfMonth);
            var startDate = dueDate.AddMonths(1);

            var eMIDetails = new EMIDetailsViewModel
            {
                EMIHeader = new EMIHeader()
                {
                    EMIAmount = emi,
                    LoanAmount = eMIConfig.LoanAmount,
                    MonthlyRateOfInterest = eMIConfig.MonthlyRateOfInterest,
                    NoOfInstallment = eMIConfig.NoOfInstallment,
                    LockInPeriod = eMIConfig.LockInPeriod,
                    StartTime = startDate,
                    EndTime = startDate.AddMonths(eMIConfig.NoOfInstallment-1),
                    //*new Variables Added*//
                    BankId = eMIConfig.BankId,
                    UserAccountId = eMIConfig.UserAccountId,
                    EMIType = (int)Enumeration.EMIType.LoanEMI,
                    LoanStatus = (int)Enumeration.LoanStatus.Pending,
                    InstallmentDayOfMonth = bank.InstallmentDayOfMonth,
                    DelayFine = bank.LoanDelayFine,
                    DelayFineType= bank.LoanDelayFineType,
                    DelayFinePeriod = bank.LoanDelayFineTerm,
                    InterestTermId = bank.InterestTermID
                }
            };
            eMIDetails.Installments = installmentComponent.GetInstallments(eMIDetails);
            eMIDetails.BankName = bank.BankName;
            eMIDetails.UserAccountName = _context.UserAccount.Where(x=>x.UserAccountId == eMIDetails.EMIHeader.UserAccountId).First().UserAccountName;

            return View("EMIDetails", eMIDetails);
        }

        public ActionResult View(int Id)
        {
            var eMIHeader = _context.EMIHeaders.SingleOrDefault(c => c.EMIHeaderId == Id);

            if (eMIHeader == null)
                return NotFound();

            var eMIDetails = new EMIDetailsViewModel()
            {
                EMIHeader = eMIHeader,
                Installments = _context.Installments.Where(c => c.EMIHeaderId == Id).ToList(),
                BankName = _context.Bank.Where(x => x.BankId == eMIHeader.BankId).First().BankName,
                UserAccountName = _context.UserAccount.Where(x => x.UserAccountId == eMIHeader.UserAccountId).First().UserAccountName
            };

            return View("EMIDetails", eMIDetails);
        }

        //public ActionResult Edit(int Id)
        //{
        //    var eMIHeader = _context.EMIHeaders.SingleOrDefault(c => c.EMIHeaderId == Id);

        //    if (eMIHeader == null)
        //        return NotFound();

        //    var viewModel = new EMIDetailsViewModel()
        //    {
        //        EMIHeader = eMIHeader,
        //        Installments = _context.Installments.Where(c => c.EMIHeaderId == Id).ToList()
        //    };

        //    return View("EMIDetails", viewModel);
        //}
    }
}
