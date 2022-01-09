using BudgetManager.BusinessComponents;
using BudgetManager.Data;
using BudgetManager.Models;
using BudgetManager.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static BudgetManager.Models.Enumeration;

namespace BudgetManager.Controllers
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
                eMIHeaderViewModel.GroupId = eMIHeader.GroupId;
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
                eMIHeaderViewModel.GroupName = _context.Group.Where(x => x.GroupId == eMIHeader.GroupId).First().GroupName;
                eMIHeaderViewModel.UserAccountName = _context.UserAccount.Where(x => x.UserAccountId == eMIHeader.UserAccountId).First().UserAccountName;

                eMIHeaderViewModelList.Add(eMIHeaderViewModel);
            }

            return View("List", eMIHeaderViewModelList);
        }

        public ActionResult New()
        {
            var eMIConfigViewModel = new EMIConfigViewModel()
            {
                Groups = _context.Group.ToList()
            };
            return View("EMIConfig", eMIConfigViewModel);
        }

        public async Task<IActionResult> Save(EMIDetailsViewModel eMIDetails)
        {
            var userAccount = _context.UserAccount.Where(x => x.UserAccountId == eMIDetails.EMIHeader.UserAccountId).First();

            if (userAccount.AmountOnLoan != 0)
            {
                throw new NotImplementedException("Active loan pending, Validation to be implemented");
            }

            installmentComponent.SaveInstallments(eMIDetails.EMIHeader, eMIDetails.Installments);


            var moneytransaction = new Transaction
            {
                GroupId = eMIDetails.EMIHeader.GroupId,
                UserAccountId = eMIDetails.EMIHeader.UserAccountId,
                TransactionTypeId = (int)TransactionType.Credit,
                TransactionAmount = eMIDetails.EMIHeader.LoanAmount,
                TransactionDate = DateTime.Now,
                ReferenceType = CreditRefType.IndividualLoan.ToString(),
                ReferenceTypeId = (int)CreditRefType.IndividualLoan
            };

            await new MoneyTransactionComponent(_context).CreateMoneyTransaction(moneytransaction);

            return RedirectToAction(nameof(Index));
        }

        public ActionResult EMIDetails(EMIConfigViewModel eMIConfig)
        {
            var users = _context.UserAccount.Where(x => x.GroupId == eMIConfig.GroupId).ToList();

            var totalShare = users.Sum(x => x.ShareSubmitted);
            var totalFine = users.Sum(x => x.FineSubmitted);
            var totalInterest = users.Sum(x => x.InterestSubmitted);
            var totalAmtOnLoan = users.Sum(x => x.AmountOnLoan);
            var TotalAmtInGroup = totalShare + totalFine + totalInterest - totalAmtOnLoan;

            if (eMIConfig.LoanAmount > TotalAmtInGroup)
            {
                throw new NotImplementedException(string.Format("Loan Amt more than the available Amt in Group. Amt available : {0}, Validation to be implemented", TotalAmtInGroup));
            }
            eMIConfig.EMIType = (int)EMIType.LoanEMI;
            EMIDetailsViewModel eMIDetailsViewModel = new EMIDetailsViewModel();
            eMIDetailsViewModel.EMIHeader = installmentComponent.GetEMIHeader(eMIConfig);
            eMIDetailsViewModel.Installments = installmentComponent.GetInstallments(eMIDetailsViewModel.EMIHeader);

            eMIDetailsViewModel.GroupName = _context.Group.Where(x => x.GroupId == eMIDetailsViewModel.EMIHeader.GroupId).First().GroupName;
            eMIDetailsViewModel.UserAccountName = _context.UserAccount.Where(x => x.UserAccountId == eMIDetailsViewModel.EMIHeader.UserAccountId).First().UserAccountName;

            return View("EMIDetails", eMIDetailsViewModel);
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
                GroupName = _context.Group.Where(x => x.GroupId == eMIHeader.GroupId).First().GroupName,
                UserAccountName = _context.UserAccount.Where(x => x.UserAccountId == eMIHeader.UserAccountId).First().UserAccountName
            };

            return View("EMIDetails", eMIDetails);
        }
    }
}
