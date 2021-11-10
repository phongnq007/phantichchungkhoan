using Microsoft.EntityFrameworkCore;
using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace PhanTichChungKhoan.Infrastructure.DbContexts
{
    public class PriceBoardDbContext : IdentityDbContext
    {
        public PriceBoardDbContext(DbContextOptions<PriceBoardDbContext> options)
           : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<IdentityUser>().Property(p => p.Id).HasMaxLength(200);
            modelBuilder.Entity<IdentityUser>().Property(p => p.ConcurrencyStamp).HasMaxLength(2000);
            modelBuilder.Entity<IdentityRole>().Property(p => p.Id).HasMaxLength(200);
            modelBuilder.Entity<IdentityRole>().Property(p => p.ConcurrencyStamp).HasMaxLength(2000);
        }
    }
}
