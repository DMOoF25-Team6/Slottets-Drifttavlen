// Copyright (c) 2026 Team6. All rights reserved.
//  No warranty, explicit or implicit, provided.

namespace Core.Interfaces.Services;

/// <summary>
/// Generates deterministic pseudonyms for GDPR-protected identifiers (UC-010).
/// </summary>
/// <remarks>
/// Implements pseudonymisation per GDPR Article 4(5): replacement of identifying
/// data with an alias derived from a secret key, such that the original identifier
/// cannot be recovered without access to that key.
///
/// Datatilsynet's "Pseudonymisering og anonymisering" catalogue and EU Court ruling
/// of 22 June 2023 require pseudonymised data to remain personal data under GDPR;
/// the salt/key MUST be stored separately from the pseudonymised records.
///
/// For MedicineRecord retention, Autorisationsloven §22 mandates that the medical
/// journal be preserved for at least 10 years. The deterministic property of this
/// service ensures that pseudonyms remain stable across logs, preserving journal
/// integrity while removing direct identifiability of the data subject.
/// </remarks>
public interface IPseudonymizationService
{
    /// <summary>
    /// Returns a deterministic pseudonym for the given identifier.
    /// </summary>
    /// <param name="identifier">The original identifier (e.g. a resident GUID).</param>
    /// <returns>A 32-character lowercase hex pseudonym derived via HMAC-SHA256.</returns>
    string Pseudonymize(string identifier);

    /// <summary>
    /// Returns a short pseudonym suitable for human-readable display fields (e.g. initials).
    /// </summary>
    /// <param name="identifier">The original identifier.</param>
    /// <returns>The first two uppercase hex characters of the pseudonym.</returns>
    string PseudonymizeShort(string identifier);
}
