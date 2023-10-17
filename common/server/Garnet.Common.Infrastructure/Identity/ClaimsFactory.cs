using System.Security.Claims;

namespace Garnet.Common.Infrastructure.Identity;

public static class ClaimsFactory
{
    public static ClaimsPrincipal User(string userId, string schemaName)
    {
        return BuildClaims(userId, schemaName);
    }
    
    public static ClaimsPrincipal System(string schemaName)
    {
        return BuildClaims("system", schemaName);
    }

    private static ClaimsPrincipal BuildClaims(string nameIdentifier, string schemaName)
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, nameIdentifier),
        };
        var identity = new ClaimsIdentity(claims, schemaName);
        return new ClaimsPrincipal(identity);
    }
}