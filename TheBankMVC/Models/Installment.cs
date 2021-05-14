using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
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
        public int EMIType { get; set; }
        public int InstallmentNo { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime DueDate { get; set; }
        [DisplayFormat(DataFormatString = "{0:dd MMM yyyy}")]
        public DateTime? PaymentDate { get; set; }
        public double Opening { get; set; }
        public double PrincipalAmount { get; set; }
        public double InterestAmount { get; set; }
        public double EMIAmount { get; set; }
        public double Closing { get; set; }
        public double Difference { get; set; }
        public int InstallmentStatus { get; set; }
        public double Fine { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}