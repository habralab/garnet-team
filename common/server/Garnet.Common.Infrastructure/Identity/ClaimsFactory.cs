using System.Security.Claims;

namespace Garnet.Common.Infrastructure.Identity;

public static class ClaimsFactory
{
    public static ClaimsPrincipal System(string schemaName = "Default")
    {
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, "system"),
            new (ClaimTypes.Name, "Система")
        };
        var identity = new ClaimsIdentity(claims, schemaName);
        return new ClaimsPrincipal(identity);
    }
}