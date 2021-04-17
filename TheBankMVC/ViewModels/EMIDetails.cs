using TheBankMVC.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBankMVC.ViewModels
{
    public class EMIDetails
    {
        public EMIHeader EMIHeader { get; set; }
        public EMIConfig EMIConfig { get; set; }
        public TimePeriod TimePeriod { get; set; }
        public IEnumerable<Installment> Installments { get; set; }
    }
}
