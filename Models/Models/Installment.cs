using System;
using System.ComponentModel;

namespace BudgetManager.Models
{
    public class Installment
    {
        public int GroupId { get; set; }
        public int UserAccountId { get; set; }
        public int Id { get; set; }
        public int EMIHeaderId { get; set; }
        public int EMIType { get; set; }
        
        [DisplayName("S.No")]
        public int InstallmentNo { get; set; }
        public DateTime DueDate { get; set; }
        public DateTime? PaymentDate { get; set; }
        public double Opening { get; set; }

        [DisplayName("Principal")]
        public double PrincipalAmount { get; set; }

        [DisplayName("Interest")]
        public double InterestAmount { get; set; }
        public double EMIAmount { get; set; }
        public double Closing { get; set; }
        public double Difference { get; set; }

        [DisplayName("Inst.Status")]
        public int InstallmentStatus { get; set; }
        public double Fine { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}