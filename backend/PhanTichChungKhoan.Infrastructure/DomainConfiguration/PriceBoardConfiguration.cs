using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhanTichChungKhoan.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhanTichChungKhoan.Infrastructure.DomainConfiguration
{
    public class PriceBoardConfiguration : IEntityTypeConfiguration<PriceBoard>
    {
        public void Configure(EntityTypeBuilder<PriceBoard> builder)
        {
            builder.HasKey(p => new { p.Exchange, p.Symbol });
            builder.Property(p => p.Exchange).HasColumnType("NVARCHAR(10)");
            builder.Property(p => p.Symbol).HasColumnType("NVARCHAR(20)");
            builder.Property(p => p.Price).IsRequired();
            builder.Property(p => p.UpdatedDate).IsRequired();
            builder.Property(p => p.CompanyName).HasColumnType("NVARCHAR(2000)");
        }
    }
}
