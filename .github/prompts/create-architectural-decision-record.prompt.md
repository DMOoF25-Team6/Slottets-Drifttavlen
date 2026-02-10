---
agent: 'agent'
description: 'Create an Architectural Decision Record (ADR) document for AI-optimized decision documentation.'
tools: ['changes', 'search/codebase', 'edit/editFiles', 'extensions', 'web/fetch', 'githubRepo', 'openSimpleBrowser', 'problems', 'runTasks', 'search', 'search/searchResults', 'runCommands/terminalLastCommand', 'runCommands/terminalSelection', 'testFailure', 'usages', 'vscodeAPI']
---
# Create Architectural Decision Record

Create an ADR document for `${input:DecisionTitle}` using structured formatting optimized for AI consumption and human readability.

## Inputs

- **Context**: `${input:Context}`
- **Decision**: `${input:Decision}`
- **Alternatives**: `${input:Alternatives}`
- **Stakeholders**: `${input:Stakeholders}`

## Input Validation
If any of the required inputs are not provided or cannot be determined from the conversation history, ask the user to provide the missing information before proceeding with ADR generation.

## Requirements

- Use precise, unambiguous language
- Follow standardized ADR format with front matter
- Each ADR must have a unique sequential identifier
- Include both positive and negative consequences
- Document alternatives with rejection rationale
- Structure for machine parsing and human reference
- Use coded bullet points (3-4 letter codes + 3-digit numbers) for multi-item sections

The ADR must be saved in the `/docs/adr/` directory using the naming convention: `adr-NNNN-[title-slug].md`, where NNNN is the next sequential 4-digit number (e.g., `adr-0001-database-selection.md`).

## Required Documentation Structure

The documentation file must follow the template below, ensuring that all sections are filled out appropriately. The front matter for the markdown should be structured correctly as per the example following:

```md
---
title: "ADR-NNNN: [Decision Title]"
status: "Proposed"
date: "YYYY-MM-DD"
authors: "[Stakeholder Names/Roles]"
tags: ["architecture", "decision"]
supersedes: ""
superseded_by: ""
---

# ADR-NNNN: [Decision Title]

## Status

**Proposed** | Accepted | Rejected | Superseded | Deprecated

## Context

[Problem statement, technical constraints, business requirements, and environmental factors requiring this decision.]

## Decision

[Chosen solution with clear rationale for selection.]

## Consequences

### Positive

- **ADR-NNNN-POS-001**: [Beneficial outcomes and advantages]
- **ADR-NNNN-POS-002**: [Performance, maintainability, scalability improvements]
- **ADR-NNNN-POS-003**: [Alignment with architectural principles]

### Negative

- **ADR-NNNN-NEG-001**: [Trade-offs, limitations, drawbacks]
- **ADR-NNNN-NEG-002**: [Technical debt or complexity introduced]
- **ADR-NNNN-NEG-003**: [Risks and future challenges]

## Alternatives Considered

### [Alternative 1 Name]

- **ADR-NNNN-ALT-001**: **Description**: [Brief technical description]
- **ADR-NNNN-ALT-002**: **Rejection Reason**: [Why this option was not selected]

### [Alternative 2 Name]

- **ADR-NNNN-ALT-003**: **Description**: [Brief technical description]
- **ADR-NNNN-ALT-004**: **Rejection Reason**: [Why this option was not selected]

## Implementation Notes

- **ADR-NNNN-IMP-001**: [Key implementation considerations]
- **ADR-NNNN-IMP-002**: [Migration or rollout strategy if applicable]
- **ADR-NNNN-IMP-003**: [Monitoring and success criteria]

## References

- **ADR-NNNN-REF-001**: [Related ADRs]
- **ADR-NNNN-REF-002**: [External documentation]
- **ADR-NNNN-REF-003**: [Standards or frameworks referenced]
```
