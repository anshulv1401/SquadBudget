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
            var eMIHeader = _context.EMIHeaders.ToList();
            return View("List", eMIHeader);
        }

        public ActionResult New()
        {
            return View("EMIConfig");
        }

        public ActionResult Save(EMIViewModel eMIDetails)
        {
            using (var transaction = _context.Database.BeginTransaction())
            {
                try
                {
                    _context.EMIHeaders.Add(eMIDetails.EMIHeader);
                    _context.SaveChanges();

                    eMIDetails.Installments.ForEach(x => x.EMIHeaderId = eMIDetails.EMIHeader.EMIHeaderId);

                    _context.Installments.AddRange(eMIDetails.Installments);
                    _context.SaveChanges();

                    transaction.Commit();
                }
                catch (Exception ex)
                {
                    transaction.Rollback();
                    //TODO log the error
                    throw ex;
                }
            }

            return View("List", _context.EMIHeaders.ToList());
        }

        public ActionResult EMIDetails(EMIConfigViewModel eMIConfig)
        {
            var r = eMIConfig.MonthlyRateOfInterest / 100;//Monthly RateOfInterest
            var t = eMIConfig.NoOfInstallment;
            var p = eMIConfig.LoanAmount;

            var rPlus1PowN = Math.Pow((1 + r), t);
            var emi = (int)((p * r * rPlus1PowN) / (rPlus1PowN - 1));

            var eMIDetails = new EMIViewModel
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
            eMIDetails.Installments = installmentComponent.GetInstallments(eMIDetails);

            return View("EMIDetails", eMIDetails);
        }

        public ActionResult View(int Id)
        {
            var eMIHeaders = _context.EMIHeaders.SingleOrDefault(c => c.EMIHeaderId == Id);

            if (eMIHeaders == null)
                return NotFound();

            var eMIDetails = new EMIViewModel()
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

            var viewModel = new EMIViewModel()
            {
                EMIHeader = eMIHeader,
                Installments = _context.Installments.Where(c => c.EMIHeaderId == Id).ToList()
            };

            return View("EMIDetails", viewModel);
        }
    }
}
