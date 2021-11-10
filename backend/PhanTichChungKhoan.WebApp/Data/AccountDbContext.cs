using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace PhanTichChungKhoan.WebApp.Data
{
    //public class AccountDbContext : IdentityDbContext
    //{
    //    public AccountDbContext(DbContextOptions<AccountDbContext> options)
    //        : base(options)
    //    {
    //    }

    //    protected override void OnModelCreating(ModelBuilder builder)
    //    {
    //        base.OnModelCreating(builder);

    //        builder.Entity<IdentityUser>().Property(p => p.Id).HasMaxLength(200);
    //        builder.Entity<IdentityUser>().Property(p => p.ConcurrencyStamp).HasMaxLength(2000);
    //        builder.Entity<IdentityRole>().Property(p => p.Id).HasMaxLength(200);
    //        builder.Entity<IdentityRole>().Property(p => p.ConcurrencyStamp).HasMaxLength(2000);
    //    }
    //}
}
