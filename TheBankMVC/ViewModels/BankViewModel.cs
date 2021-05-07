using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using TheBankMVC.Models;

namespace TheBankMVC.ViewModels
{
    public class BankViewModel
    {
        public int BankId { get; set; }
        public string BankName { get; set; }
        public double BankInstAmt { get; set; }
        public double TotalShare { get; set; }
        public double TotalFine { get; set; }
        public double TotalInterest { get; set; }
        public double TotalAmtOnLoan { get; set; }
        public double TotalAmtInBank { get; set; }
    }
}
