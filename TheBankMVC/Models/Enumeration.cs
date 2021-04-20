using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBankMVC.Models
{
    public class Enumeration
    {
        public int EnumerationId { get; set; }
        public string EnumerationType { get; set; }
        public string EnumerationName { get; set; }
        public int EnumerationValue { get; set; }
    }
}
