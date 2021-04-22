using System.Collections.Generic;
using TheBankMVC.Models;

namespace TheBankMVC.ViewModels
{
    public class EMIViewModel
    {
        public EMIHeader EMIHeader { get; set; }
        public List<Installment> Installments { get; set; }
    }
}
