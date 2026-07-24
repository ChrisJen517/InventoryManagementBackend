using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace InventoryApi.Models;

public class Product
{
    public int Id { get; set; }

    // [Required]
    public string? Title { get; set; }

    // [Required]
    [DataType(DataType.Currency)]
    [Column(TypeName = "decimal(18, 2)")]
    public decimal Price { get; set; }
    public string? Description { get; set; }

    // [Required]
    [DataType(DataType.DateTime)]
    public DateTime IntakeDate { get; set; }

    // [Required]
    public int Quantity { get; set; }

    // [Required]
    public string? Status { get; set; }
    public int CategoryId { get; set; }
    public int VendorId { get; set; }
    public string? StorageLocation { get; set; }
    public string? ShipmentStatus { get; set; }
    public string? Notes { get; set; }
}