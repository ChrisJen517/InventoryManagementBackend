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
    public DbSet<Category> Categories { get; set; }
    public DbSet<Location> Locations { get; set; }
    public DbSet<Shipment> Shipments { get; set; }
    public DbSet<Vendor> Vendors { get; set; }
    public DbSet<UserIdentity> UserIdentities { get; set; }


    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        SeedRoles(builder);

        builder.Entity<Product>()
            .HasOne(p => p.Vendor)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.VendorId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Product>()
            .HasOne(p => p.Category)
            .WithMany(u => u.Products)
            .HasForeignKey(p => p.CategoryId)
            .OnDelete(DeleteBehavior.SetNull);

        builder.Entity<Category>()
            .HasOne(p => p.Vendor)
            .WithMany(u => u.Categories)
            .HasForeignKey(p => p.VendorId)
            .OnDelete(DeleteBehavior.SetNull);


        builder.Entity<UserIdentity>()
            .HasOne(u => u.Vendor)
            .WithMany(v => v.UserIdentities)
            .HasForeignKey(u => u.VendorId)
            .OnDelete(DeleteBehavior.SetNull);

    }

    private void SeedRoles(ModelBuilder builder)
    {
        builder.Entity<IdentityRole>().HasData(
            new IdentityRole() { Name = "Admin", ConcurrencyStamp = "1", NormalizedName = "Admin" },
            new IdentityRole() { Name = "Vendor", ConcurrencyStamp = "2", NormalizedName = "Vendor" }
        );
    }
}