using System.Collections.Generic;
using System.ComponentModel;
using BudgetManager.Models;

namespace BudgetManager.ViewModels
{
    public class EMIDetailsViewModel
    {
        [DisplayName("SquadName")]
        public string GroupName { get; set; }

        [DisplayName("MemberName")]
        public string UserAccountName { get; set; }
        public EMIHeader EMIHeader { get; set; }
        public List<Installment> Installments { get; set; }
    }
}
