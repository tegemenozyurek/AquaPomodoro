using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AquaPomodoro.Data;

namespace AquaPomodoro.Controllers;

[ApiController]
[Route("api/[controller]")]
public class HealthController : ControllerBase
{
    private readonly AquaPomodoroDbContext _context;
    private readonly ILogger<HealthController> _logger;

    public HealthController(AquaPomodoroDbContext context, ILogger<HealthController> logger)
    {
        _context = context;
        _logger = logger;
    }

    // GET: api/health/database
    [HttpGet("database")]
    public async Task<ActionResult<object>> CheckDatabaseConnection()
    {
        try
        {
            // Test database connection by executing a simple query
            var canConnect = await _context.Database.CanConnectAsync();
            
            if (!canConnect)
            {
                return Ok(new
                {
                    status = "Failed",
                    message = "Cannot connect to database",
                    timestamp = DateTime.UtcNow,
                    database = "PostgreSQL"
                });
            }

            // Try to execute a simple query to verify schema access
            var userCount = await _context.Users.CountAsync();
            var fishCount = await _context.Fishes.CountAsync();
            var aquariumCount = await _context.Aquariums.CountAsync();

            _logger.LogInformation("Database connection check successful");

            return Ok(new
            {
                status = "Healthy",
                message = "Database connection is working",
                timestamp = DateTime.UtcNow,
                database = "PostgreSQL",
                schema = "AquariumPomodoro",
                statistics = new
                {
                    users = userCount,
                    fishes = fishCount,
                    aquariums = aquariumCount
                }
            });
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Database connection check failed");

            return StatusCode(500, new
            {
                status = "Error",
                message = "Database connection failed",
                error = ex.Message,
                timestamp = DateTime.UtcNow,
                database = "PostgreSQL"
            });
        }
    }

    // GET: api/health/app
    [HttpGet("app")]
    public ActionResult<object> CheckApplicationHealth()
    {
        return Ok(new
        {
            status = "Healthy",
            message = "Application is running",
            timestamp = DateTime.UtcNow,
            environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Production",
            version = "1.0.0"
        });
    }

    // GET: api/health
    [HttpGet]
    public async Task<ActionResult<object>> CheckOverallHealth()
    {
        var appHealthy = true;
        var dbHealthy = false;
        var errors = new List<string>();

        try
        {
            // Check database
            dbHealthy = await _context.Database.CanConnectAsync();
            if (!dbHealthy)
            {
                errors.Add("Database connection failed");
            }
        }
        catch (Exception ex)
        {
            errors.Add($"Database error: {ex.Message}");
        }

        var overallHealthy = appHealthy && dbHealthy;

        return Ok(new
        {
            status = overallHealthy ? "Healthy" : "Unhealthy",
            timestamp = DateTime.UtcNow,
            checks = new
            {
                application = appHealthy ? "Healthy" : "Unhealthy",
                database = dbHealthy ? "Healthy" : "Unhealthy"
            },
            errors = errors.Count > 0 ? errors : null
        });
    }
} 