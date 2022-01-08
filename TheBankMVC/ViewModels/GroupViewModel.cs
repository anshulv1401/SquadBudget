namespace BudgetManager.ViewModels
{
    public class GroupViewModel
    {
        public int GroupId { get; set; }
        public string GroupName { get; set; }
        public double GroupInstAmt { get; set; }
        public double TotalShare { get; set; }
        public double TotalFine { get; set; }
        public double TotalInterest { get; set; }
        public double TotalAmtOnLoan { get; set; }
        public double TotalAmtInGroup { get; set; }
    }
}
