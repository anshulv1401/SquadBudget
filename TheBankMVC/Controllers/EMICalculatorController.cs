using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using TheBankMVC.Models;
using TheBankMVC.ViewModels;

namespace TheBankMVC.Controllers
{
    public class EMICalculatorController : Controller
    {

        private ApplicationDbContext _context;
        public EMICalculatorController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            var eMIHeader = _context.EMIHeaders.ToList();
            return View("List", eMIHeader);
        }

        public ActionResult New()
        {
            return View("EMIConfig");
        }

        public ActionResult Save(EMIDetails eMIDetails)
        {
            EMIHeader eMIHeader = eMIDetails.EMIHeader;
            _context.EMIHeaders.Add(eMIDetails.EMIHeader);
            _context.SaveChanges();
            eMIDetails.Installments.ForEach(x => x.EMIHeaderId = eMIHeader.EMIHeaderId);
            _context.Installments.AddRange(eMIDetails.Installments);
            _context.SaveChanges();
            return View("List", _context.EMIHeaders.ToList());
        }

        public ActionResult EMIDetails(EMIConfig eMIConfig)
        {
            var r = eMIConfig.MonthlyRateOfInterest / 100;//Monthly RateOfInterest
            var t = eMIConfig.NoOfInstallment;
            var p = eMIConfig.LoanAmount;

            var rPlus1PowN = Math.Pow((1 + r), t);
            var emi = (int)((p * r * rPlus1PowN) / (rPlus1PowN - 1));

            var eMIDetails = new EMIDetails
            {
                EMIHeader = new EMIHeader()
                {

                    EMIAmount = emi,
                    LoanAmount = eMIConfig.LoanAmount,
                    MonthlyRateOfInterest = eMIConfig.MonthlyRateOfInterest,
                    NoOfInstallment = eMIConfig.NoOfInstallment,
                    LockInPeriod = eMIConfig.LockInPeriod,
                    StartTime = DateTime.Now.AddMonths(1),
                    EndTime = DateTime.Now.AddMonths(1 + eMIConfig.NoOfInstallment),
                }
            };
            eMIDetails.Installments = GetInstallments(eMIDetails);

            return View("EMIDetails", eMIDetails);
        }

        private List<Installment> GetInstallments(EMIDetails eMIDetails)
        {
            var installments = new List<Installment>();
            var dateOfInstallment = eMIDetails.EMIHeader.StartTime;
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

        public ActionResult View(int Id)
        {
            var eMIHeaders = _context.EMIHeaders.SingleOrDefault(c => c.EMIHeaderId == Id);

            if (eMIHeaders == null)
                return NotFound();

            var eMIDetails = new EMIDetails()
            {
                EMIHeader = eMIHeaders,
                Installments = _context.Installments.Where(c => c.EMIHeaderId == Id).ToList(),
            };

            return View("EMIDetails", eMIDetails);
        }

        public ActionResult Edit(int Id)
        {
            var eMIHeader = _context.EMIHeaders.SingleOrDefault(c => c.EMIHeaderId == Id);

            if (eMIHeader == null)
                return NotFound();

            var viewModel = new EMIDetails()
            {
                EMIHeader = eMIHeader,
                Installments = _context.Installments.Where(c => c.EMIHeaderId == Id).ToList()
            };

            return View("EMIDetails", viewModel);
        }
    }
}
