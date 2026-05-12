// Copyright (c) 2026 Team6. All rights reserved. 
//  No warranty, explicit or implicit, provided.

using Core.Interfaces.Repositories;

using Domain.Entities;
using Domain.Enums;

namespace Api.BackgroundServices;

/// <summary>
/// Periodically scans for residents whose data has exceeded its retention period
/// and creates AnonymizationCandidate records for Admin review (UC-010).
/// </summary>
/// <remarks>
/// Runs once every 24 hours by default. Does NOT anonymize data directly —
/// always requires Admin approval through the Anonymization Queue.
/// </remarks>
public class RetentionBackgroundService : BackgroundService
{
    #region Fields

    private readonly IServiceProvider _serviceProvider;
    private readonly ILogger<RetentionBackgroundService> _logger;
    private readonly TimeSpan _interval = TimeSpan.FromHours(24);

    #endregion

    #region Constructors

    public RetentionBackgroundService(
        IServiceProvider serviceProvider,
        ILogger<RetentionBackgroundService> logger)
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
        _logger.LogInformation("RetentionBackgroundService started.");

        while (!stoppingToken.IsCancellationRequested)
        {
            try
            {
                await ScanForCandidatesAsync(stoppingToken);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in retention scan loop.");
            }

            await Task.Delay(_interval, stoppingToken);
        }

        _logger.LogInformation("RetentionBackgroundService stopped.");
    }

    /// <summary>
    /// Scans residents against retention policies and creates pending anonymization candidates.
    /// </summary>
    /// <remarks>
    /// STUB: production should evaluate per-resident inactivity against retention periods
    /// and add AnonymizationCandidate entries via repository. For now logs the scan for traceability.
    /// </remarks>
    private async Task ScanForCandidatesAsync(CancellationToken cancellationToken)
    {
        using IServiceScope scope = _serviceProvider.CreateScope();
        IRetentionPolicyRepository repository = scope.ServiceProvider.GetRequiredService<IRetentionPolicyRepository>();

        IEnumerable<RetentionPolicy> policies = await repository.GetAllAsync(cancellationToken);
        _logger.LogInformation(
            "Retention scan completed. Loaded {Count} active retention policies.",
            policies.Count());
    }

    #endregion
}
