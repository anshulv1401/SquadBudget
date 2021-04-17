using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using TheBankMVC.Models;
using TheBankMVC.ViewModels;

namespace TheBankMVC.Controllers
{
    public class EMICalculatorController : Controller
    {

        private ApplicationDbContext _context;
        public EMICalculatorController()
        {
            //var builder = new ConfigurationBuilder()
            //    .SetBasePath(Directory.GetCurrentDirectory())
            //    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            //IConfigurationRoot configuration = builder.Build();

            //var optionsBuilder = new DbContextOptionsBuilder();

            //optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));

            //var context = new DbContext(optionsBuilder.Options);

            //context.Database.EnsureCreated();

            _context = new ApplicationDbContext();
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View("EMIConfig");
        }

        public ActionResult Save(EMIDetails eMIDetails)
        {
            EMIHeader eMIHeader = eMIDetails.EMIHeader;
            _context.EMIHeaders.Add(eMIDetails.EMIHeader);
            _context.Installments.AddRange(eMIDetails.Installments);
            _context.SaveChanges();
            return View("Index");
        }

        public ActionResult EMIDetails(EMIConfig eMIConfig)
        {
            eMIConfig = new EMIConfig()
            {
                MonthlyRateOfInterest = 0.5,
                NoOfInstallment = 10,
                LoanAmount = 60000
            };

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
                    DateFormat = "dd-MMM-yyyy"
                }
            };
            eMIDetails.Installments = GetInstallments(eMIDetails);
            eMIDetails.EMIHeader.Installments = eMIDetails.Installments;

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
                    DateOfInstallment = dateOfInstallment,
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
    }
}
