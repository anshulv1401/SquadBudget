﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBankMVC.Models
{
    public class Installment
    {
        public int BankId { get; set; }
        public int UserAccountId { get; set; }
        public int Id { get; set; }
        public int EMIHeaderId { get; set; }
        public int InstallmentNo { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public double Opening { get; set; }
        public double PrincipalAmount { get; set; }
        public double InterestAmount { get; set; }
        public double EMIAmount { get; set; }
        public double Closing { get; set; }
        public double Difference { get; set; }
        public int InstallmentStatus { get; set; }
        public double Fine { get; set; }
    }
}