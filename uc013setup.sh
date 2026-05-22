#!/usr/bin/env bash
# UC-013 — Show current user in status bar
# Run from anywhere; cd's into project automatically.
set -euo pipefail

cd ~/Developer/Slottets-Drifttavlen

git checkout main
git pull
git checkout -b 578-uc013-code-webui

mkdir -p docs/use-cases/uc-013 \
         src/WebUI/WebUI.Client/Components \
         tests/WebUI.Tests/Components

# ===================== USE CASE =====================
cat > docs/use-cases/uc-013/use-case.md <<'DOC_EOF'
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
DOC_EOF

# ===================== WIREFRAME =====================
cat > docs/use-cases/uc-013/wireframe.md <<'DOC_EOF'
# UC-013 — Wireframe

## Authenticated
```
+-----------------------------------------------------------------+
| (page content)                                                  |
+-----------------------------------------------------------------+
| DB GREEN  |  USER test@example.com                  (c) 2026   |
+-----------------------------------------------------------------+
```

## Not authenticated
```
+-----------------------------------------------------------------+
| DB RED    |  USER Ikke logget ind                    (c) 2026  |
+-----------------------------------------------------------------+
```
DOC_EOF

# ===================== DCD =====================
cat > docs/use-cases/uc-013/dcd.md <<'DOC_EOF'
# UC-013 — Design Class Diagram

```mermaid
classDiagram
    class MainLayout { +IsDbConnected: bool }
    class CurrentUserStatus { -GetDisplayName(ClaimsPrincipal) string }
    class AuthorizeView
    class AuthenticationStateProvider {
        <<abstract>>
        +GetAuthenticationStateAsync()
    }
    class JwtAuthenticationStateProvider
    class AuthenticationState { +User: ClaimsPrincipal }
    class ClaimsPrincipal { +FindFirst(string) Claim }

    MainLayout --> CurrentUserStatus : renders
    CurrentUserStatus --> AuthorizeView : uses
    AuthorizeView ..> AuthenticationStateProvider : consumes
    JwtAuthenticationStateProvider --|> AuthenticationStateProvider
    AuthenticationState --> ClaimsPrincipal
```
DOC_EOF

# ===================== SEQUENCE DIAGRAM =====================
cat > docs/use-cases/uc-013/sd.md <<'DOC_EOF'
# UC-013 — Sequence Diagram

```mermaid
sequenceDiagram
    actor U as User
    participant ML as MainLayout
    participant CUS as CurrentUserStatus
    participant AV as AuthorizeView
    participant ASP as JwtAuthenticationStateProvider

    U->>ML: Open page
    ML->>CUS: Render
    CUS->>AV: AuthorizeView
    AV->>ASP: GetAuthenticationStateAsync()
    ASP-->>AV: AuthenticationState
    alt Authenticated
        AV->>CUS: Authorized(context.User)
        CUS->>CUS: GetDisplayName(user)
        CUS-->>U: USER email
    else Not authenticated
        AV->>CUS: NotAuthorized
        CUS-->>U: USER Ikke logget ind
    end
```
DOC_EOF

# ===================== COMPONENT (.razor) =====================
cat > src/WebUI/WebUI.Client/Components/CurrentUserStatus.razor <<'RAZOR_EOF'
@rendermode InteractiveWebAssembly
@using Microsoft.AspNetCore.Components.Authorization

<AuthorizeView>
    <Authorized>
        <span title="Aktuel bruger" class="border-end px-3">
            <span>&#x1F464;</span>
            <span class="ms-1">@GetDisplayName(context.User)</span>
        </span>
    </Authorized>
    <NotAuthorized>
        <span title="Ikke logget ind" class="border-end px-3 text-muted">
            <span>&#x1F464;</span>
            <span class="ms-1">Ikke logget ind</span>
        </span>
    </NotAuthorized>
</AuthorizeView>
RAZOR_EOF

# ===================== COMPONENT (.razor.cs) =====================
cat > src/WebUI/WebUI.Client/Components/CurrentUserStatus.razor.cs <<'CS_EOF'
using System.Security.Claims;

namespace WebUI.Client.Components;

public partial class CurrentUserStatus
{
    private static string GetDisplayName(ClaimsPrincipal user)
    {
        string? email = user.FindFirst(ClaimTypes.Email)?.Value;
        string? name = user.FindFirst(ClaimTypes.Name)?.Value;
        return email ?? name ?? "Ukendt bruger";
    }
}
CS_EOF

# ===================== MAIN LAYOUT =====================
cat > src/WebUI/WebUI/Components/Layout/MainLayout.razor <<'RAZOR_EOF'
@using WebUI.Client.Components
@inherits LayoutComponentBase

<NavBar />

<main class="container mt-4 flex-fill">
  <ErrorBoundary>
    <ChildContent>
      @Body
    </ChildContent>
    <ErrorContent>
      <div id="blazor-error-ui" class="alert alert-danger mt-4">
        <h5>An unhandled error has occurred.</h5>
        <p><b>Message:</b> @context.Message</p>
        <p><b>Stack Trace:</b></p>
        <pre style="white-space: pre-wrap;">@context.StackTrace</pre>
        <a href="" class="reload">Reload</a>
        <a class="dismiss">X</a>
      </div>
    </ErrorContent>
  </ErrorBoundary>

  <script src="/_framework/aspnetcore-browser-refresh.js"></script>
</main>

<footer class="footer bg-light mt-auto">
  <div class="d-flex justify-content-between align-items-center w-100 px-3">
    <span title="Database forbindelse" class="fw-bold border-end pe-3">
      <span>&#x1F5C4;</span>
      @if (IsDbConnected)
      {
        <span style="color:green;font-size:1.2em;vertical-align:middle;">&#x1F7E2;</span>
      }
      else
      {
        <span style="color:red;font-size:1.2em;vertical-align:middle;">&#x1F534;</span>
      }
    </span>
    <CurrentUserStatus />
    <span class="ps-3">
      &copy; 2026 - Team6
    </span>
  </div>
</footer>
RAZOR_EOF

# ===================== UNIT TESTS =====================
cat > tests/WebUI.Tests/Components/CurrentUserStatusTests.cs <<'CS_EOF'
using Bunit;
using Bunit.TestDoubles;
using System.Security.Claims;
using WebUI.Client.Components;
using Xunit;

namespace WebUI.Tests.Components;

public class CurrentUserStatusTests : Bunit.TestContext
{
    [Fact]
    public void ShowsEmail_WhenAuthenticated_WithEmailClaim()
    {
        TestAuthorizationContext authContext = this.AddTestAuthorization();
        authContext.SetAuthorized("Test User");
        authContext.SetClaims(new Claim(ClaimTypes.Email, "test@example.com"));

        IRenderedComponent<CurrentUserStatus> cut = RenderComponent<CurrentUserStatus>();

        Assert.Contains("test@example.com", cut.Markup);
    }

    [Fact]
    public void ShowsName_WhenAuthenticated_WithoutEmailClaim()
    {
        TestAuthorizationContext authContext = this.AddTestAuthorization();
        authContext.SetAuthorized("Test User");

        IRenderedComponent<CurrentUserStatus> cut = RenderComponent<CurrentUserStatus>();

        Assert.Contains("Test User", cut.Markup);
    }

    [Fact]
    public void ShowsNotLoggedIn_WhenNotAuthenticated()
    {
        TestAuthorizationContext authContext = this.AddTestAuthorization();
        authContext.SetNotAuthorized();

        IRenderedComponent<CurrentUserStatus> cut = RenderComponent<CurrentUserStatus>();

        Assert.Contains("Ikke logget ind", cut.Markup);
    }
}
CS_EOF

echo ""
echo "=========================================="
echo "  UC-013 filer oprettet"
echo "=========================================="
ls -la docs/use-cases/uc-013/
ls -la src/WebUI/WebUI.Client/Components/CurrentUserStatus.*
ls -la tests/WebUI.Tests/Components/CurrentUserStatusTests.cs
echo ""
echo "Naeste skridt: docker compose --profile test build build-stage"
