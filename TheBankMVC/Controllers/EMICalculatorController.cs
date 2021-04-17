using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TheBankMVC.Models;
using TheBankMVC.ViewModels;

namespace TheBankMVC.Controllers
{
    public class EMICalculatorController : Controller
    {

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult New()
        {
            return View("EMIConfig");
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
                EMIConfig = eMIConfig,
                TimePeriod = new TimePeriod()
                {
                    StartTime = DateTime.Now.AddMonths(1),
                    EndTime = DateTime.Now.AddMonths(1 + eMIConfig.NoOfInstallment),
                    DateFormat = "dd-MMM-yyyy"
                },
                EMIHeader = new EMIHeader()
                {
                    EMIAmount = emi,
                    MonthlyRateOfInterest = eMIConfig.MonthlyRateOfInterest
                }
            };
            eMIDetails.Installments = GetInstallments(eMIDetails);

            return View("EMIDetails", eMIDetails);
        }

        private List<Installment> GetInstallments(EMIDetails eMIDetails)
        {
            var installments = new List<Installment>();
            var dateOfInstallment = eMIDetails.TimePeriod.StartTime;
            var opening = eMIDetails.EMIConfig.LoanAmount;
            var eMIAmount = eMIDetails.EMIHeader.EMIAmount;
            var interestAmount = opening * (eMIDetails.EMIConfig.MonthlyRateOfInterest / 100);
            var principalAmount = eMIAmount - interestAmount;
            var closing = opening - principalAmount;
            var difference = 0.0;


            for (int i = 1; i <= eMIDetails.EMIConfig.NoOfInstallment; i++)
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
                interestAmount = opening * (eMIDetails.EMIConfig.MonthlyRateOfInterest / 100);
                principalAmount = eMIAmount - interestAmount;
                closing = opening - principalAmount;

                if (i == eMIDetails.EMIConfig.NoOfInstallment - 1)//for last EMI
                {
                    difference = closing;
                }
            }
            return installments;
        }
    }
}
