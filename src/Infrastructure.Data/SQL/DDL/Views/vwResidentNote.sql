-- ============================================================
-- View: vwResidentNote
-- Description: Provides a read-optimized view of resident notes
--              joined with the resident's initials so the API
--              can return notes with display data in a single query.
--              Used by the Dashboard ResidentNote feature.
-- CrossReference: UC-002.ERD UC-002.DCD
-- Author: Team 6
-- Date: 2026-05-03
-- Version: 0001
-- ============================================================

CREATE OR REPLACE VIEW vwResidentNote AS
    -- Join ResidentNotes with Residents to resolve ResidentId to Initials
    SELECT
        rn.Id           AS Id,
        rn.Note         AS Note,
        rn.CreatedAt    AS CreatedAt,
        rn.EditedAt     AS EditedAt,
        rn.ResidentId   AS ResidentId,
        r.Initials      AS Initials
    FROM ResidentNotes rn
    INNER JOIN Residents r ON rn.ResidentId = r.Id;
