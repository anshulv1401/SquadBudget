using System.Collections.Generic;
using BudgetManager.Models;

namespace BudgetManager.ViewModels
{
    public class EMIDetailsViewModel
    {
        public string GroupName { get; set; }
        public string UserAccountName { get; set; }
        public EMIHeader EMIHeader { get; set; }
        public List<Installment> Installments { get; set; }
    }
}
