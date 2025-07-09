using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AquaPomodoro.Data;
using AquaPomodoro.Models;

namespace AquaPomodoro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AquaPomodoroDbContext _context;

    public AuthController(AquaPomodoroDbContext context)
    {
        _context = context;
    }

    // POST: api/auth/login
    [HttpPost("login")]
    public async Task<ActionResult<object>> Login([FromBody] LoginRequest loginRequest)
    {
        if (string.IsNullOrEmpty(loginRequest.Mail) || string.IsNullOrEmpty(loginRequest.Password))
        {
            return BadRequest(new { message = "Email and password are required" });
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Mail == loginRequest.Mail);

        if (user == null)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        // Simple password check (in production, use proper password hashing)
        if (user.Password != loginRequest.Password)
        {
            return Unauthorized(new { message = "Invalid email or password" });
        }

        // Return user info (excluding password)
        return Ok(new
        {
            message = "Login successful",
            user = new
            {
                id = user.Id,
                mail = user.Mail,
                coinBalance = user.CoinBalance,
                totalFocusTime = user.TotalFocusTime,
                createdAt = user.CreatedAt
            }
        });
    }

    // GET: api/auth/check-email/{email}
    [HttpGet("check-email/{email}")]
    public async Task<ActionResult<object>> CheckEmailExists(string email)
    {
        var exists = await _context.Users.AnyAsync(u => u.Mail == email);
        
        return Ok(new
        {
            exists = exists,
            message = exists ? "Email already exists" : "Email is available"
        });
    }
}

public class LoginRequest
{
    public string Mail { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
} 