using System.Text.Encodings.Web;
using Garnet.Common.Infrastructure.Support;
using Microsoft.AspNetCore.Authentication;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace Garnet.Common.Infrastructure.Identity.Kratos;

public class KratosAuthenticationHandler : AuthenticationHandler<KratosAuthenticationSchemeOptions>
{
    private readonly KratosProvider _kratosProvider;
    private const string SessionCookieName = "ory_kratos_session";

    public KratosAuthenticationHandler(
        IOptionsMonitor<KratosAuthenticationSchemeOptions> options, 
        ILoggerFactory logger, 
        UrlEncoder encoder, 
        ISystemClock clock,
        KratosProvider kratosProvider
    ) : base(options, logger, encoder, clock)
    {
        _kratosProvider = kratosProvider;
    }

    protected override async Task<AuthenticateResult> HandleAuthenticateAsync()
    {
        try
        {
            if (Request.Cookies.TryGetValue(SessionCookieName, out var cookie))
            {
                var result = await _kratosProvider.GetUserIdentityIdByCookie(CancellationToken.None, SessionCookieName, cookie);
                result.ThrowQueryExceptionIfHasErrors();
                return await BuildAuthenticationResult(result.Value);
            }

            if (Request.Headers.TryGetValue("Authorization", out var token))
            {
                var result = await _kratosProvider.GetUserIdentityIdByToken(CancellationToken.None, token.ToString());
                result.ThrowQueryExceptionIfHasErrors();
                return await BuildAuthenticationResult(result.Value);
            }

            return AuthenticateResult.NoResult();
        }
        catch (Exception ex)
        {
            return AuthenticateResult.Fail(ex.Message);
        }
    }
    
    private Task<AuthenticateResult> BuildAuthenticationResult(string userIdentityId)
    {
        var principal = ClaimsFactory.User(userIdentityId, Scheme.Name);
        var ticket = new AuthenticationTicket(principal, Scheme.Name);
        return Task.FromResult(AuthenticateResult.Success(ticket));
    } 
}