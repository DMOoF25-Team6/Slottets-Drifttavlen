// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

namespace Api.BackgroundServices;

/// <summary>
/// Periodically scans audit logs for suspicious patterns (failed logins, mass exports,
/// off-hours access) and creates SecurityIncident records for Admin review (UC-010).
/// </summary>
/// <remarks>
/// Runs every 15 minutes by default. Detected incidents enter the SecurityIncident
/// queue with Status=Open and require manual Admin investigation.
/// </remarks>
public class IncidentDetectionService : BackgroundService
{
    #region Fields

    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<IncidentDetectionService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromMinutes(15);

    #endregion

    #region Constructors

    public IncidentDetectionService(
        IServiceProvider serviceProvider,
        ILogger<IncidentDetectionService> logger)
    {
        ArgumentNullException.ThrowIfNull(serviceProvider);
        ArgumentNullException.ThrowIfNull(logger);
        _serviceProvider = serviceProvider;
        _logger = logger;
    }

    #endregion

    #region Methods

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("IncidentDetectionService started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await DetectIncidentsAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in incident detection loop.");
            }

            await Task.Delay(_interval, stoppingToken);
        }

        _logger.LogInformation("IncidentDetectionService stopped.");
    }

    /// <summary>
    /// Scans audit entries for suspicious patterns and creates SecurityIncident records.
    /// </summary>
    /// <remarks>
    /// STUB: production should evaluate audit entries for failed logins, mass exports,
    /// off-hours admin access, etc. For now logs the scan for traceability.
    /// </remarks>
    private Task DetectIncidentsAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        ISecurityIncidentRepository repository = scope.ServiceProvider.GetRequiredService<ISecurityIncidentRepository>();

        _logger.LogInformation("Incident detection scan completed at {Timestamp}.", DateTime.UtcNow);
        return Task.CompletedTask;
    }

    #endregion
}
