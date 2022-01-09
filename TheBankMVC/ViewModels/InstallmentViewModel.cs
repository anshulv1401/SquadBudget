using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BudgetManager.ViewModels
{
    public class InstallmentViewModel
    {
        [DisplayName("SquadName")]
        public string GroupName { get; set; }

        [DisplayName("MemberName")]
        public string UserAccountName { get; set; }
        public int Id { get; set; }
        public int EMIHeaderId { get; set; }
        public int EMIType { get; set; }

        [DisplayName("S.no")]
        public int InstallmentNo { get; set; }
        
        [DisplayFormat(DataFormatString = "{0:dd MMM yy}")]
        [DisplayName("Inst.DueDate")]
        public DateTime DueDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd MMM yy}")]
        public DateTime? PaymentDate { get; set; }
        public double Opening { get; set; }

        [DisplayName("Principal")]
        public double PrincipalAmount { get; set; }

        [DisplayName("Interest")]
        public double InterestAmount { get; set; }

        [DisplayName("EMI")]
        public double EMIAmount { get; set; }

        [DisplayName("Diff")]
        public double Difference { get; set; }
        public double Closing { get; set; }

        [DisplayName("Status")]
        public int InstallmentStatus { get; set; }
        public double Fine { get; set; }
    }
}
