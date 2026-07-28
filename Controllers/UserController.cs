using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using InventoryApi.Areas.Identity.Data;
using InventoryApi.Models;
using System.Text.Json;

namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("api/user")]
    [Authorize] // Enforces that the request must be authenticated
    public class UserController : ControllerBase
    {
        [HttpGet("user-info")]
        public IActionResult GetCurrentUser()
        {
            // Retrieve data directly from token claims or session identity
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.Identity?.Name;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;

            if (userId == null)
            {
                return NotFound("User information not found in the token.");
            }

            return Ok(new
            {
                Id = userId,
                Username = username,
                Email = email
            });
        }

        private readonly UserManager<UserIdentity> _userManager;
        private readonly ApplicationDbContext _context;

        public UserController(UserManager<UserIdentity> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        [Authorize]
        [HttpPost("assign-admin")]
        public async Task<IActionResult> AssignAdminRole(string userId)
        {
            // Find the user by ID
            var user = await _userManager.FindByIdAsync(userId);

            if (user == null)
            {
                return NotFound("User not found.");
            }

            // Add user to the specified role
            IdentityResult result = await _userManager.AddToRoleAsync(user, "Admin");

            if (result.Succeeded)
            {
                return Ok("Role assigned successfully.");
            }

            // Handle failure (e.g., role doesn't exist)
            return BadRequest(result.Errors);
        }



        [Authorize]
        [HttpPost("assign-vendor")]
        public async Task<IActionResult> AssignVendor(string id, int vendorId)
        {

            if (User.IsInRole("Admin"))
            {
                return NoContent();
            }

            // Find the user by ID
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound("No User Found");
            }


            var existingVendor = await _context.Vendors.FindAsync(vendorId);

            if (existingVendor == null)
            {
                return NotFound("Vendor not found.");
            }

            user.VendorId = vendorId;

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (result.Succeeded)
            {
                return Ok("Vendor assigned successfully.");
            }

            return NoContent();
        }
    }
}