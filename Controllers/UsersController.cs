using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using InventoryApi.Areas.Identity.Data;
using InventoryApi.Models;
using System.Text.Json;
using Microsoft.EntityFrameworkCore;

namespace InventoryApi.Controllers
{
    [ApiController]
    [Route("api/users")]
    [Authorize] // Enforces that the request must be authenticated
    public class UsersController : ControllerBase
    {

        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UserIdentity>>> GetUsers([FromQuery] string? search)
        {

            var filteredUsers = _userManager.Users.AsQueryable();

            if (!string.IsNullOrEmpty(search))
            {
                string cleanSearch = search.Trim().ToLower();
                filteredUsers = filteredUsers.Where(u => u.Name.ToLower().Contains(cleanSearch) ||
                                                                u.Email.ToLower().Contains(cleanSearch));
            }
            return await filteredUsers.Include(u => u.Vendor).ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UserIdentity>> GetUser(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInfoAsync(string id, UserIdentity userdto)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null) return NotFound();

            if (string.IsNullOrWhiteSpace(userdto.Email))
            {
                return BadRequest("Email cannot be empty.");
            }

            var emailUpdated = false;
            if (userdto.Email != user.Email)
            {
                var setEmailResult = await _userManager.SetEmailAsync(user, userdto.Email);
                if (!setEmailResult.Succeeded)
                {
                    return BadRequest(setEmailResult.Errors);
                }

                await _userManager.SetUserNameAsync(user, userdto.Email);
                emailUpdated = true;
            }

            if (emailUpdated)
            {
                user = await _userManager.FindByIdAsync(id);
            }
            if (!string.IsNullOrWhiteSpace(userdto.Name))
            {
                user.Name = userdto.Name;
            }
            if (userdto.VendorId.HasValue)
            {
                user.VendorId = userdto.VendorId;
            }

            IdentityResult result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest("Could not update user.");
            }

            return Ok("Profile updated.");
        }

        [HttpGet("user-info")]
        public IActionResult GetCurrentUser()
        {
            // Retrieve data directly from token claims or session identity
            var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
            var username = User.Identity?.Name;
            var email = User.FindFirst(ClaimTypes.Email)?.Value;
            var role = User.FindFirst(ClaimTypes.Role)?.Value;

            if (userId == null)
            {
                return NotFound("User information not found in the token.");
            }

            return Ok(new
            {
                Id = userId,
                Username = username,
                Email = email,
                Role = role
            });
        }

        private readonly UserManager<UserIdentity> _userManager;
        private readonly ApplicationDbContext _context;

        public UsersController(UserManager<UserIdentity> userManager, ApplicationDbContext context)
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