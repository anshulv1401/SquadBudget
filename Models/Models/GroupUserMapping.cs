using System;

namespace BudgetManager.Models
{
    public class GroupUserMapping
    {
        public int GroupUserMappingId { get; set; }
        public int GroupId { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public string CreatedBy { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string ModifiedBy { get; set; }
    }
}
