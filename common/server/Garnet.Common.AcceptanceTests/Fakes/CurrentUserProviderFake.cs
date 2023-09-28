using System.Security.Claims;
using Garnet.Common.Application;

namespace Garnet.Common.AcceptanceTests.Fakes;

public class CurrentUserProviderFake : ICurrentUserProvider
{
    private readonly Dictionary<string, string> _users = new()
    {
        {"Система", "system"}
    };

    public string RegisterUser(string name, string id)
    {
        _users[name] = id;
        return id;
    }

    public ClaimsPrincipal LoginAsSystem()
    {
        return LoginAs("Система");
    }

    public ClaimsPrincipal LoginAs(string name)
    {
        UserId = _users[name];
        var claims = new List<Claim>
        {
            new (ClaimTypes.NameIdentifier, UserId),
            new (ClaimTypes.Name, name)
        };
        var identity = new ClaimsIdentity(claims, "Tests");
        return new ClaimsPrincipal(identity);
    }

    public string GetUserName(string id)
    {
        var username = _users.Where(x => x.Value == id).FirstOrDefault().Key;
        return username;
    }
    
    public string UserId { get; private set; } = "system";
}