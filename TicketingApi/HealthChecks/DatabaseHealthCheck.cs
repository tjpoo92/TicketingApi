using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Options;
using TicketingApi.Models;

namespace TicketingApi.HealthChecks;

public class DatabaseHealthCheck : IHealthCheck
{
    private readonly ConnectionStringOptions _connectionStringOptions;
    private readonly ILogger<DatabaseHealthCheck> _logger;

    public DatabaseHealthCheck(IOptions<ConnectionStringOptions> connectionStringOptions, ILogger<DatabaseHealthCheck> logger)
    {
        _connectionStringOptions = connectionStringOptions.Value;
        _logger = logger;
    }

    public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
    {
        Exception? firstException = null;

        // First attempt
        try
        {
            using var connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionStringOptions.DefaultConnection);
            await connection.OpenAsync(cancellationToken);
            
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT 1";
            await command.ExecuteScalarAsync(cancellationToken);
            
            return HealthCheckResult.Healthy("Database is accessible");
        }
        catch (Exception ex)
        {
            firstException = ex;
            _logger.LogWarning(ex, "Database health check failed on first attempt, attempting retry...");
        }

        // Retry attempt
        try
        {
            
            // Add a small delay before retry to allow for cold start
            await Task.Delay(1000, cancellationToken);
            
            using var connection = new Microsoft.Data.SqlClient.SqlConnection(_connectionStringOptions.DefaultConnection);
            await connection.OpenAsync(cancellationToken);
            
            using var command = connection.CreateCommand();
            command.CommandText = "SELECT 1";
            await command.ExecuteScalarAsync(cancellationToken);
            
            _logger.LogInformation("Database health check succeeded on retry attempt");
            
            return HealthCheckResult.Healthy("Database is accessible (recovered after retry)", 
                new Dictionary<string, object>
                {
                    { "retry_attempted", true },
                    { "retry_successful", true },
                    { "initial_failure_reason", firstException?.Message ?? "Unknown" }
                });
        }
        catch (Exception retryEx)
        {
            _logger.LogError(retryEx, "Database health check failed on retry attempt");
            
            return HealthCheckResult.Unhealthy("Database is not accessible after retry", 
                retryEx, 
                new Dictionary<string, object>
                {
                    { "retry_attempted", true },
                    { "retry_successful", false },
                    { "initial_failure_reason", firstException?.Message ?? "Unknown" },
                    { "retry_failure_reason", retryEx.Message }
                });
        }
    }
}
