using System.Security.Claims;

namespace TaskMgr.WebApi.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static Guid GetUserId(this ClaimsPrincipal principal)
    {
        if (principal == null) throw new ArgumentNullException(nameof(principal));

        var claim = principal.FindFirst(ClaimTypes.NameIdentifier);
        return Guid.TryParse(claim.Value, out var result) ? result : Guid.Empty;
    }
}