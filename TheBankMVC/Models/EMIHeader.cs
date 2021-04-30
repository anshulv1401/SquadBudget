﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBankMVC.Models
{
    public class EMIHeader
    {
        public int EMIHeaderId { get; set; }
        public int BankId { get; set; }
        public int UserAccountId { get; set; }
        public double EMIAmount { get; set; }
        public double LoanAmount { get; set; }
        public double MonthlyRateOfInterest { get; set; }
        public int NoOfInstallment { get; set; }
        public int LockInPeriod { get; set; }
        public int LoanStatus { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public double DelayFine { get; set; }
        public int DelayFineType { get; set; }
        public int DelayFinePeriod { get; set; }
        public int InterestTermId { get; set; }
    }
}
