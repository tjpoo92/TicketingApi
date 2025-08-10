using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace TicketingApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class HealthController : ControllerBase
{
    private readonly HealthCheckService _healthCheckService;

    public HealthController(HealthCheckService healthCheckService)
    {
        _healthCheckService = healthCheckService;
    }

    /// <summary>
    /// Get the health status of the API and its dependencies
    /// </summary>
    /// <returns>Health check report including database connectivity status</returns>
    /// <response code="200">API is healthy and all dependencies are accessible</response>
    /// <response code="503">One or more dependencies are unhealthy</response>
    [HttpGet]
    [ProducesResponseType(typeof(HealthReport), 200)]
    [ProducesResponseType(typeof(HealthReport), 503)]
    public async Task<IActionResult> GetHealth()
    {
        var report = await _healthCheckService.CheckHealthAsync();
        
        var result = new
        {
            status = report.Status.ToString(),
            totalDuration = report.TotalDuration.ToString(),
            timestamp = DateTime.UtcNow,
            checks = report.Entries.Select(entry => new
            {
                name = entry.Key,
                status = entry.Value.Status.ToString(),
                description = entry.Value.Description,
                duration = entry.Value.Duration.ToString(),
                data = entry.Value.Data,
                tags = entry.Value.Tags
            })
        };

        return report.Status == HealthStatus.Healthy ? Ok(result) : StatusCode(503, result);
    }

    /// <summary>
    /// Get a simple health status (lightweight check)
    /// </summary>
    /// <returns>Simple health status</returns>
    /// <response code="200">API is running</response>
    [HttpGet("ping")]
    [ProducesResponseType(200)]
    public IActionResult Ping()
    {
        return Ok(new { status = "healthy", message = "API is running", timestamp = DateTime.UtcNow });
    }
}
