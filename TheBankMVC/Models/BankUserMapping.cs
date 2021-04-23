using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheBankMVC.Models
{
    public class BankUserMapping
    {
        public int BankUserMappingId { get; set; }
        public int BankId { get; set; }
        public string UserId { get; set; }
    }
}
