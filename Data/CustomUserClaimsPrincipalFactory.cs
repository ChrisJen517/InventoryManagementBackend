using System.Security.Claims;
using InventoryApi.Areas.Identity.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<UserIdentity>
{
    public CustomClaimsPrincipalFactory(
        UserManager<UserIdentity> userManager,
        // RoleManager<UserIdentity> roleManager,
        IOptions<IdentityOptions> optionsAccessor)
        : base(userManager, optionsAccessor) { }

    protected override async Task<ClaimsIdentity> GenerateClaimsAsync(UserIdentity user)
    {
        var identity = await base.GenerateClaimsAsync(user);

        // Add your custom VendorId database property as a claim
        identity.AddClaim(new Claim("VendorId", user.VendorId.ToString()));

        return identity;
    }
}
