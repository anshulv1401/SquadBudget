using System.Collections.Generic;
using TheBankMVC.Models;

namespace TheBankMVC.ViewModels
{
    public class EMIDetailsViewModel
    {
        public string BankName { get; set; }
        public string UserAccountName { get; set; }
        public EMIHeader EMIHeader { get; set; }
        public List<Installment> Installments { get; set; }
    }
}
