// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

using System.Security.Cryptography;
using System.Text;

using Core.Interfaces.Services;

using Microsoft.Extensions.Configuration;

namespace Core.Services;

/// <summary>
/// HMAC-SHA256 based pseudonymisation service (UC-010).
/// </summary>
/// <remarks>
/// Cryptographic choice rationale:
///   - Datatilsynet (DK) and Datatilsynet (NO, "Anonymisering av personopplysninger",
///     2015) recommend SHA-2 with at least 256-bit strength.
///   - HMAC (instead of plain hash) prevents rainbow-table attacks and binds the
///     pseudonym to a secret salt, satisfying GDPR Art. 32(1)(a) "pseudonymisation
///     and encryption of personal data" as appropriate technical measure.
///
/// Salt management:
///   - The salt is read from the environment variable GDPR_PSEUDO_SALT.
///   - It MUST be kept outside the database (typically Docker secret / Key Vault).
///   - Rotating the salt invalidates existing pseudonyms; coordinate any rotation
///     with the Data Protection Officer.
///
/// Determinism:
///   - The same input always yields the same pseudonym.
///   - Required for MedicineRecord pseudonymisation so that medical journal entries
///     remain correlatable across the 10-year retention mandated by Autorisationsloven §22.
/// </remarks>
public class PseudonymizationService : IPseudonymizationService
{
    private const string SaltEnvironmentVariable = "GDPR_PSEUDO_SALT";

    private readonly byte[] _salt;

    public PseudonymizationService(IConfiguration configuration)
    {
        ArgumentNullException.ThrowIfNull(configuration);

        string? salt = configuration[SaltEnvironmentVariable]
                       ?? Environment.GetEnvironmentVariable(SaltEnvironmentVariable);

        if (string.IsNullOrWhiteSpace(salt))
        {
            // Fail fast: pseudonymisation without a configured secret salt is equivalent
            // to a public hash and provides no GDPR Art. 4(5) protection.
            throw new InvalidOperationException(
                $"Environment variable {SaltEnvironmentVariable} is required for GDPR pseudonymisation (UC-010).");
        }

        _salt = Encoding.UTF8.GetBytes(salt);
    }

    public string Pseudonymize(string identifier)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(identifier);

        using HMACSHA256 hmac = new(_salt);
        byte[] hash = hmac.ComputeHash(Encoding.UTF8.GetBytes(identifier));
        return Convert.ToHexString(hash)[..32].ToLowerInvariant();
    }

    public string PseudonymizeShort(string identifier)
    {
        string full = Pseudonymize(identifier);
        return full[..2].ToUpperInvariant();
    }
}
