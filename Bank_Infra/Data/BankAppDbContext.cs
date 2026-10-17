using Bank_Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bank_Infra.Data
{
    public class BankAppDbContext(DbContextOptions<BankAppDbContext> Option) : DbContext(Option)
    {
        public DbSet<Bank> Banks { get; set; } = null!;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BankAppDbContext).Assembly);
        }
    }
}
