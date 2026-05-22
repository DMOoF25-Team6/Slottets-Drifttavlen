# UC-013 — Show current user in status bar

| Key | Value |
|-----|-------|
| ID | uc-013 |
| Gateway | Gateway 10: Status Bar |
| Cross-reference | REQ-U-002, REQ-F-005 |

## Actor
- Authenticated user (staff, caretaker, admin)

## Preconditions
- User has logged in (UC-004); JWT contains `Email` or `Name` claim.

## Postconditions
- Status bar shows the user's display name on every page.

## Main flow
1. User opens any page.
2. `MainLayout` renders the footer/status bar.
3. `<CurrentUserStatus>` reads `AuthenticationState`.
4. If authenticated -> displays email (fallback to name).
5. If not authenticated -> displays "Ikke logget ind".

## Alternative flow
- A1 Token expires -> `AuthenticationStateProvider` notifies -> component re-renders as "Ikke logget ind".

## Acceptance criteria
- [ ] Visible in footer on all pages
- [ ] Shows email when authenticated
- [ ] Shows "Ikke logget ind" when not authenticated
- [ ] Updates reactively on login/logout
