using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryApi.Areas.Identity.Data;
using System.Text.Json.Serialization;

namespace InventoryApi.Models;

public class Product
{
    public int Id { get; set; }

    // [Required]
    public string? Title { get; set; }
    public string? Sku { get; set; }

    // [Required]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; } = 0;
    public string? Description { get; set; } = null;

    // [Required]
    [DataType(DataType.DateTime)]
    public DateTime IntakeDate { get; set; } = DateTime.Now;

    // [Required]
    public int Quantity { get; set; } = 0;

    // [Required]
    public string? Status { get; set; } = null;
    public int? CategoryId { get; set; } = null;
    public int? LocationId { get; set; } = null;
    public string? ShipmentStatus { get; set; } = null;
    public string? Notes { get; set; } = null;

    public int? VendorId { get; set; } = null;

    [ForeignKey(nameof(VendorId))]
    public Vendor? Vendor { get; set; }

    [ForeignKey(nameof(CategoryId))]
    public Category? Category { get; set; } = null;

    [ForeignKey(nameof(LocationId))]
    public Location? Location { get; set; } = null;
}