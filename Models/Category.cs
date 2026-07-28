using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryApi.Areas.Identity.Data;
using System.Text.Json.Serialization;

namespace InventoryApi.Models;

public class Category
{
    public int Id { get; set; }

    [Required]
    public string? Name { get; set; }
    public string? Notes { get; set; }
    public int? VendorId { get; set; } = null;

    [ForeignKey(nameof(VendorId))]
    public Vendor? Vendor { get; set; } = null;


    [JsonIgnore]
    public ICollection<Product> Products { get; set; } = new List<Product>();

}