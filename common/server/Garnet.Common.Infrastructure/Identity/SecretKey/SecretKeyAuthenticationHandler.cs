using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Net.Http.Headers;

namespace Garnet.Common.Infrastructure.Identity.SecretKey;

public class SecretKeyAuthenticationHandler : AuthenticationHandler<SecretKeyAuthenticationSchemeOptions>
{
    public SecretKeyAuthenticationHandler(
        IOptionsMonitor<SecretKeyAuthenticationSchemeOptions> options,
        ILoggerFactory logger, 
        UrlEncoder encoder, 
        ISystemClock clock) 
        : base(options, logger, encoder, clock)
    {
    }

    protected override Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        if (Request.Headers.TryGetValue(HeaderNames.Authorization, out var actual))
        {
            var expected = Environment.GetEnvironmentVariable("GARNET_API_KEY");
            if (actual == expected)
            {
                var principal = ClaimsFactory.System(Scheme.Name);
                var ticket = new AuthenticationTicket(principal, Scheme.Name);
                return Task.FromResult(AuthenticateResult.Success(ticket));
            }
        }
        
        return Task.FromResult(AuthenticateResult.Fail("Bad api key"));
    }
}