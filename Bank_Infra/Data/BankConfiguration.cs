using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Bank_Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bank_Infra.Data
{
    public class BankConfiguration : IEntityTypeConfiguration<Bank>
    {
        
        public void Configure(EntityTypeBuilder<Bank> builder)
        {
            builder.HasKey(bn => bn.Bank_ID);
            builder.Property(bn => bn.BankName).IsRequired().HasMaxLength(100);
            builder.Property(bn=> bn.BankType).IsRequired().HasMaxLength(100);
            builder.Property(bis => bis.IsActive).IsRequired();
        
        }

    }
}
