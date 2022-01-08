using BudgetManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace BudgetManager.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<EMIHeader> EMIHeaders { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Group> Group { get; set; }
        public DbSet<Enumeration> Enumerations { get; set; }
        public DbSet<GroupUserMapping> GroupUserMappings { get; set; }
        public DbSet<UserAccount> UserAccount { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IHttpContextAccessor httpContextAccessor)
            : base(options)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public string GetCurrentUserId()
        {
            return _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value;
        }
    }
}
