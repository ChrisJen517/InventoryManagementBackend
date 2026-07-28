using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;
using InventoryApi.Areas.Identity.Data;

namespace InventoryApi.Models;

public class Shipment
{
    public int Id { get; set; }
    public string? TrackingCode { get; set; }
    public int ProductId { get; set; }
    public string? Address { get; set; } = null;
    public string? City { get; set; } = null;
    public string? State { get; set; } = null;
    public string? Zip { get; set; } = null;

    [JsonIgnore]
    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }

}