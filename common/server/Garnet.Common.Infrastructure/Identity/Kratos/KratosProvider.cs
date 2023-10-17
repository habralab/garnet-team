using Garnet.Common.Infrastructure.Identity.Kratos.Errors;
using Garnet.Common.Infrastructure.Support;
using Ory.Kratos.Client.Api;
using Ory.Kratos.Client.Client;
using Result = FluentResults.Result;

namespace Garnet.Common.Infrastructure.Identity.Kratos;

public class KratosProvider
{
    // API Docs: https://github.com/ory/sdk/blob/master/clients/kratos/dotnet/README.md
    private readonly ICourierApiAsync _courierApi;
    private readonly IFrontendApiAsync _frontendApi;
    private readonly IIdentityApiAsync _identityApi;
    
    public KratosProvider()
    {
        var kratosConfig = new Configuration
        {
            BasePath = EnvironmentEx.GetRequiredEnvironmentVariable("KRATOS_URL"),
            ApiKey = new Dictionary<string, string>
            {
                { "Authorization", EnvironmentEx.GetRequiredEnvironmentVariable("KRATOS_API_KEY") }
            },
            ApiKeyPrefix = new Dictionary<string, string>
            {
                { "Authorization", "Bearer" }
            }
        };

        _courierApi = new CourierApi(kratosConfig);
        _frontendApi = new FrontendApi(kratosConfig);
        _identityApi = new IdentityApi(kratosConfig);
    }
    
    public async Task<FluentResults.Result<string>> GetUserIdentityIdByCookie(CancellationToken ct, string sessionCookieName, string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return Result.Fail(new MissingAuthTokenError("Отсутствует авторизационный токен в cookies"));
        }
        
        var session = await _frontendApi.ToSessionAsync(token, sessionCookieName, ct);
        return session.Active
                   ? Result.Ok(session.Identity.Id)
                   : Result.Fail(new SessionIsNotActiveError());
    }

    public async Task<FluentResults.Result<string>> GetUserIdentityIdByToken(CancellationToken ct, string? token)
    {
        if (string.IsNullOrWhiteSpace(token))
        {
            return Result.Fail(new MissingAuthTokenError("Отсутствует авторизационный токен"));
        }
        
        var session = await _frontendApi.ToSessionAsync(token, cancellationToken: ct);
        return session.Active
                   ? Result.Ok(session.Identity.Id)
                   : Result.Fail(new SessionIsNotActiveError());
    }
}