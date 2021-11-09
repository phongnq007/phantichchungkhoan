using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PhanTichChungKhoan.Domain;
using System;
using System.Collections.Generic;
using System.Text;

namespace PhanTichChungKhoan.Infrastructure.DomainConfiguration
{
    public class MyPriceBoardConfiguration : IEntityTypeConfiguration<MyPriceBoard>
    {
        public void Configure(EntityTypeBuilder<MyPriceBoard> builder)
    {
        builder.HasKey(p => new { p.UserId, p.Exchange, p.Symbol });
            builder.Property(p => p.UserId).HasMaxLength(200);
            builder.Property(p => p.Exchange).HasColumnType("NVARCHAR(10)");
            builder.Property(p => p.Symbol).HasColumnType("NVARCHAR(20)");
        }
}
}
