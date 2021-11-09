using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhanTichChungKhoan.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhanTichChungKhoan.Infrastructure.DomainConfiguration
{
    public class HnxPriceBoardTempConfiguration : IEntityTypeConfiguration<HnxPriceBoardTemp>
    {
        public void Configure(EntityTypeBuilder<HnxPriceBoardTemp> builder)
        {
            builder.HasKey(p => new { p.Exchange, p.Symbol });
            builder.Property(p => p.Exchange).HasColumnType("NVARCHAR(10)");
            builder.Property(p => p.Symbol).HasColumnType("NVARCHAR(20)");
            builder.Property(p => p.Price).IsRequired();
            builder.Ignore(p => p.UpdatedDate);
            builder.Property(p => p.CompanyName).HasColumnType("NVARCHAR(2000)");

        }
    }
}
