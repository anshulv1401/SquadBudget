using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using TheBankMVC.Models;

namespace TheBankMVC.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public virtual DbSet<EMIHeader> EMIHeaders { get; set; }
        public virtual DbSet<Installment> Installments { get; set; }

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
    }
}
