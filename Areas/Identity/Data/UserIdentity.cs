using Microsoft.AspNetCore.Identity;
using InventoryApi.Models;

namespace InventoryApi.Areas.Identity.Data;

public class UserIdentity : IdentityUser
{
    [PersonalData]
    public string? Name { get; set; }

    public ICollection<Product> Products { get; set; } = new List<Product>();

}