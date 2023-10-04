using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using StockManagementSystem.Entities;
using StockManagementSystem.Models;

namespace StockManagementSystem.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid, ApplicationUserClaim, ApplicationUserRole, ApplicationUserLogin, ApplicationRoleClaim, ApplicationUserToken>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Category> CategoryList { get; set; }
        public DbSet<Company> CompanyList { get; set; }
        public DbSet<Item> ItemList { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<Company>().
                HasMany(i => i.items).
                WithOne(m => m.Company).
                HasForeignKey(e => e.CompanyId).
                IsRequired();
            builder.Entity<Category>().
               HasMany(i => i.items).
               WithOne(m => m.Category).
               HasForeignKey(e => e.CategoryId).
              IsRequired();
            builder.Entity<Item>()
            .HasIndex(e => e.Name)
            .IsUnique();
            builder.Entity<Category>()
               .HasIndex(e => e.Type)
            .IsUnique();

            builder.Entity<Company>()
             .HasIndex(e => e.Name)
          .IsUnique();

        }
    }
}