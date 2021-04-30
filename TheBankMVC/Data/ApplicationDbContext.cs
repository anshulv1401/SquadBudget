using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using System.Security.Claims;
using System.Threading.Tasks;
using TheBankMVC.Models;

namespace TheBankMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<EMIHeader> EMIHeaders { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<Enumeration> Enumerations { get; set; }
        public DbSet<BankUserMapping> BankUserMappings { get; set; }
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
