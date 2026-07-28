using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryApi.Areas.Identity.Data;
using System.Text.Json.Serialization;

namespace InventoryApi.Models;

public class Vendor
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }

    [JsonIgnore]

    public ICollection<Product> Products { get; set; } = new List<Product>();
    [JsonIgnore]
    public ICollection<Category> Categories { get; set; } = new List<Category>();
    [JsonIgnore]
    public ICollection<Location> Locations { get; set; } = new List<Location>();
    [JsonIgnore]
    public ICollection<UserIdentity> UserIdentities { get; set; } = new List<UserIdentity>();

}