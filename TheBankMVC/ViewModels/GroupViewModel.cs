using System.ComponentModel;

namespace BudgetManager.ViewModels
{
    public class GroupViewModel
    {
        public int GroupId { get; set; }

        [DisplayName("SquadName")]
        public string GroupName { get; set; }

        [DisplayName("SquadInstAmt")]
        public double GroupInstAmt { get; set; }
        public double TotalShare { get; set; }
        public double TotalFine { get; set; }
        public double TotalInterest { get; set; }
        public double TotalAmtOnLoan { get; set; }

        [DisplayName("TotalAmtInSquad")]
        public double TotalAmtInGroup { get; set; }
    }
}
