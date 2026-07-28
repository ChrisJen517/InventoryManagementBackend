using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryApi.Areas.Identity.Data;

namespace InventoryApi.Models;

public class Vendor
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }


    public ICollection<Product> Products { get; set; } = new List<Product>();
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    public ICollection<Location> Locations { get; set; } = new List<Location>();
    public ICollection<UserIdentity> UserIdentities { get; set; } = new List<UserIdentity>();

}