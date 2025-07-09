using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AquaPomodoro.Data;
using AquaPomodoro.Models;
using System.Text.Json;

namespace AquaPomodoro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsersController : ControllerBase
{
    private readonly AquaPomodoroDbContext _context;

    public UsersController(AquaPomodoroDbContext context)
    {
        _context = context;
    }

    // GET: api/users
    [HttpGet]
    public async Task<ActionResult<IEnumerable<User>>> GetUsers()
    {
        return await _context.Users.ToListAsync();
    }

    // GET: api/users/5
    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetUser(int id)
    {
        var user = await _context.Users
            .Include(u => u.Aquariums)
            .ThenInclude(a => a.Fish)
            .FirstOrDefaultAsync(u => u.Id == id);

        if (user == null)
        {
            return NotFound();
        }

        return user;
    }

    // POST: api/users
    [HttpPost]
    public async Task<ActionResult<User>> PostUser(User user)
    {
        _context.Users.Add(user);
        await _context.SaveChangesAsync();

        return CreatedAtAction("GetUser", new { id = user.Id }, user);
    }

    // PUT: api/users/5 (Partial Update Support)
    [HttpPut("{id}")]
    public async Task<IActionResult> PutUser(int id, JsonElement userUpdate)
    {
        var existingUser = await _context.Users.FindAsync(id);
        if (existingUser == null)
        {
            return NotFound();
        }

        // Update only the provided fields
        if (userUpdate.TryGetProperty("mail", out var mailProperty))
        {
            existingUser.Mail = mailProperty.GetString() ?? existingUser.Mail;
        }

        if (userUpdate.TryGetProperty("password", out var passwordProperty))
        {
            existingUser.Password = passwordProperty.GetString() ?? existingUser.Password;
        }

        if (userUpdate.TryGetProperty("coinBalance", out var coinBalanceProperty))
        {
            existingUser.CoinBalance = coinBalanceProperty.GetInt32();
        }

        if (userUpdate.TryGetProperty("totalFocusTime", out var totalFocusTimeProperty))
        {
            existingUser.TotalFocusTime = totalFocusTimeProperty.GetInt32();
        }

        try
        {
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!UserExists(id))
            {
                return NotFound();
            }
            else
            {
                throw;
            }
        }

        return NoContent();
    }

    // DELETE: api/users/5
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteUser(int id)
    {
        var user = await _context.Users.FindAsync(id);
        if (user == null)
        {
            return NotFound();
        }

        _context.Users.Remove(user);
        await _context.SaveChangesAsync();

        return NoContent();
    }

    private bool UserExists(int id)
    {
        return _context.Users.Any(e => e.Id == id);
    }
} 