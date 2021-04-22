using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.IO;
using TheBankMVC.Models;

namespace TheBankMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public DbSet<EMIHeader> EMIHeaders { get; set; }
        public DbSet<Installment> Installments { get; set; }
        public DbSet<Bank> Bank { get; set; }
        public DbSet<Enumeration> Enumerations { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public static ApplicationDbContext Create()
        {
            var builder = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);

            IConfigurationRoot configuration = builder.Build();

            var optionsBuilder = new DbContextOptionsBuilder<ApplicationDbContext>();

            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Bank_AnshulVanawat"));

            return new ApplicationDbContext(optionsBuilder.Options);
        }

        public DbSet<TheBankMVC.Models.UserAccount> UserAccount { get; set; }
    }
}
