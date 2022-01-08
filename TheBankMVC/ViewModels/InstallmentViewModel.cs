using System;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.ViewModels
{
    public class InstallmentViewModel
    {
        public string GroupName { get; set; }
        public string UserAccountName { get; set; }
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
        public double Difference { get; set; }
        public double Closing { get; set; }
        public int InstallmentStatus { get; set; }
        public double Fine { get; set; }
    }
}
