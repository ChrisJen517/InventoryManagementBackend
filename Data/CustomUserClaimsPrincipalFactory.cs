using System.Security.Claims;
using InventoryApi.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserIdentity, IdentityRole>
{
    public CustomClaimsPrincipalFactory(
        UserManager<UserIdentity> userManager,
        RoleManager<IdentityRole> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, roleManager, optionsAccessor) { }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserIdentity user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        identity.AddClaim(new Claim("VendorId", user.VendorId.ToString()));

        return identity;
    }
}
