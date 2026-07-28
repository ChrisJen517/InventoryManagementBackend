using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using InventoryApi.Areas.Identity.Data;

namespace InventoryApi.Models;

public class Shipment
{
    public int Id { get; set; }
    public string? TrackingCode { get; set; }
    public int ProductId { get; set; }



    [ForeignKey(nameof(ProductId))]
    public Product? Product { get; set; }

}