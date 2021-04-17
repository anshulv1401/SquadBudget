using System.Collections.Generic;
using TheBankMVC.Models;

namespace TheBankMVC.ViewModels
{
    public class EMIDetails
    {
        public EMIHeader EMIHeader { get; set; }
        public List<Installment> Installments { get; set; }
    }
}
