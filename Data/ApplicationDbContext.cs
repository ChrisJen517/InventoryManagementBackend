using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using InventoryApi.Models;
using InventoryApi.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;

public class ApplicationDbContext : IdentityDbContext<UserIdentity>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
    { }

    public DbSet<Product> Products { get; set; }
    public DbSet<UserIdentity> UserIdentities { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SeedRoles(builder);

        builder.Entity<Product>()
            .HasOne(p => p.UserIdentity)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }

    private void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
            new IdentityRole() { Name = "Vendor", ConcurrencyStamp = "2", NormalizedName = "Vendor" }
        );
    }
}