using Microsoft.AspNetCore.Identity;
using InventoryApi.Models;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApi.Areas.Identity.Data;

public class UserIdentity : IdentityUser
{
    [PersonalData]
    public string? Name { get; set; }

    public int? VendorId { get; set; } = null;

    // public ICollection<Product> Products { get; set; } = new List<Product>();

    [ForeignKey(nameof(VendorId))]
    public Vendor? Vendor { get; set; }
}