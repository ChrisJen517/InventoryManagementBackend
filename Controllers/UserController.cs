using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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
    }
}