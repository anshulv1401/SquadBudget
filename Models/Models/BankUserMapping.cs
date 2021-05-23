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
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
