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
