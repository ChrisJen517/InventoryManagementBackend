using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using InventoryApi.Areas.Identity.Data;

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

        public UserController(UserManager<UserIdentity> userManager)
        {
            _userManager = userManager;
        }

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
    }
}